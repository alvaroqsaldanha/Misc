import torch
import torch.nn as nn
import numpy as np
from sklearn import datasets
import matplotlib.pyplot as plt

# Preparing Data

X_numpy, Y_numpy = datasets.make_regression(n_samples=100, n_features=1,noise=20,random_state=1)

X = torch.from_numpy(X_numpy.astype(np.float32))
Y = torch.from_numpy(Y_numpy.astype(np.float32))
Y = Y.view(Y.shape[0],1)

n_samples, n_features = X.shape

# Model

model = nn.Linear(n_features,1)

loss = nn.MSELoss()
optimizer = torch.optim.SGD(model.parameters(),lr=0.01)

# Training

epochs = 100

for epoch in range(epochs):
    Y_pred = model(X)
    curr_loss = loss(Y_pred,Y)
    curr_loss.backward()
    optimizer.step()
    optimizer.zero_grad()
    if (epoch % 10 == 0):
        print(f'epoch: {epoch}, loss = {curr_loss.item()}')

final = model(X).detach()
plt.plot(X_numpy,Y_numpy,'ro')
plt.plot(X_numpy,final,'b')
plt.show()




    




