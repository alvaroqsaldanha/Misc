import torch
import torch.nn as nn
import numpy as np
from sklearn import datasets
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split

# Data Preparation
bc = datasets.load_breast_cancer()
X,y = bc.data, bc.target
n_samples, n_features = X.shape
X_trn, X_test, y_trn, y_test = train_test_split(X,y,test_size=0.2,random_state=1234)

sc = StandardScaler()
X_trn = sc.fit_transform(X_trn)
X_test = sc.fit_transform(X_test)

X_trn = torch.from_numpy(X_trn.astype(np.float32))
X_test = torch.from_numpy(X_test.astype(np.float32))
y_trn = torch.from_numpy(y_trn.astype(np.float32))
y_test = torch.from_numpy(y_test.astype(np.float32))

y_trn = y_trn.view(y_trn.shape[0],1)
y_test = y_test.view(y_test.shape[0],1)

# Model

class LogisticRegression(nn.Module):
    def __init__(self,n_input_features):
        super(LogisticRegression,self).__init__()
        self.lin = nn.Linear(n_input_features,1)
        
    def forward(self,x):
        return torch.sigmoid(self.lin(x))
    
model = LogisticRegression(n_features)
loss = nn.BCELoss()
optimizer = torch.optim.SGD(model.parameters(),lr=0.01) 

# Training

epochs = 100

for epoch in range(epochs):
    y_pred = model(X_trn)
    curr_loss = loss(y_pred,y_trn)
    curr_loss.backward()
    optimizer.step()
    optimizer.zero_grad()
    if (epoch + 1) % 10 == 1:
        print(f'epoch: {epoch}, loss: {curr_loss}')

with torch.no_grad():
    y_pred = model(X_test)
    y_pred_cls = y_pred.round()
    acc = y_pred_cls.eq(y_test).sum() / float(y_test.shape[0])
    print("Accuracy: ", acc.item())

