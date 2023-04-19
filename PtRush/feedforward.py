import torch
import torch.nn as nn
import numpy as np
import torchvision
import torchvision.transforms as transforms
import matplotlib.pyplot as plt
from sklearn.metrics import accuracy_score

# device
device = torch.device('cuda' if torch.cuda.is_available() else 'cpu') 

# hyper parameters
input_size = 784
hidden_size = 100
num_classes = 10
num_epochs = 5
batch_size = 64
lr = 0.01

# dataset loading
train_dataset = torchvision.datasets.MNIST(root='./data', train=True,transform=transforms.ToTensor(),download=True)
test_dataset = torchvision.datasets.MNIST(root='./data', train=False,transform=transforms.ToTensor(),download=True)
train_loader = torch.utils.data.DataLoader(dataset=train_dataset,batch_size=batch_size,shuffle=True)
test_loader = torch.utils.data.DataLoader(dataset=test_dataset,batch_size=batch_size,shuffle=False)

examples = iter(train_loader)
samples,labels = next(examples)
print(samples.shape,labels.shape)

for i in range(6):
    plt.subplot(2,3,i+1)
    plt.imshow(samples[i][0])
#plt.show()

class NeuralNet(nn.Module):
    def __init__(self,input_size,hidden_size,num_classes):
        super(NeuralNet,self).__init__()
        self.l1 = nn.Linear(input_size,hidden_size)
        self.a1 = nn.ReLU()
        self.l2 = nn.Linear(hidden_size,num_classes)

    def forward(self,x):
        return self.l2(self.a1(self.l1(x)))
    
model = NeuralNet(input_size,hidden_size,num_classes)

criterion = nn.CrossEntropyLoss()
optimizer = torch.optim.Adam(model.parameters(),lr=lr)

for epoch in range(num_epochs):
    for i, (images,labels) in enumerate(train_loader):
        images = images.reshape(-1,28*28).to(device)
        labels = labels.to(device)

        outputs = model(images)
        loss = criterion(outputs,labels)
        loss.backward()
        optimizer.step()
        optimizer.zero_grad()
    print(f'Epoch: {epoch}, Loss: {loss.item()}')

accuracy = 0
with torch.no_grad():
    for i, (images,labels) in enumerate(test_loader):
        images = images.reshape(-1,28*28).to(device)
        labels = labels.to(device)
        preds = torch.max(model(images),1)[1]
        accuracy += 1/(i+1) * (accuracy_score(preds,labels) - accuracy)
    print(accuracy)

        


