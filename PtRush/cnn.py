import torch
import torch.nn as nn
import torchvision
import numpy as np
from sklearn.metrics import accuracy_score
import torchvision.transforms as transforms
import matplotlib.pyplot as plt


device = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

num_epochs = 10
batch_size = 32
lr = 0.1

transform = transforms.Compose([transforms.ToTensor(),
                                transforms.Normalize((0.5,0.5,0.5),(0.5,0.5,0.5))])

train_dataset = torchvision.datasets.CIFAR10(root='./data',train=True,download=True,transform=transform)
test_dataset = torchvision.datasets.CIFAR10(root='./data',train=False,download=True,transform=transform)
train_loader = torch.utils.data.DataLoader(train_dataset,batch_size=batch_size,shuffle=True)
test_loader = torch.utils.data.DataLoader(test_dataset,batch_size=batch_size,shuffle=False)

class CNN(nn.Module):
    def __init__(self):
        super(CNN,self).__init__()
        self.conv1 = nn.Conv2d(3,32,3)
        self.pool = nn.MaxPool2d(2,2)
        self.conv2 = nn.Conv2d(32,64,3)
        self.l1 = nn.Linear(64*6*6,128)
        self.relu = nn.ReLU()
        self.l2 = nn.Linear(128,10)
    
    def forward(self,x):
        x = self.pool(self.relu(self.conv1(x)))
        x = self.pool(self.relu(self.conv2(x)))
        x = x.view(-1,64*6*6)
        return self.l2(self.relu(self.l1(x)))



model = CNN().to(device)
criterion = nn.CrossEntropyLoss()
optimizer = torch.optim.SGD(model.parameters(),lr=lr)

for epoch in range(num_epochs):
    for i, (images,labels) in enumerate(train_loader):
        images = images.to(device)
        labels = labels.to(device)
        outputs = model(images)
        loss = criterion(outputs,labels)
        loss.backward()
        optimizer.step()
        optimizer.zero_grad()
    print(f'Epoch: {epoch}, Loss: {loss}')

accuracy = 0
with torch.no_grad():
    for i, (images,labels) in enumerate(test_loader):
        images = images.to(device)
        labels = labels.to(device)
        outputs = model(images)
        _, predicted = torch.max(outputs,1)
        accuracy += 1/(i+1)*(accuracy_score(predicted,labels) - accuracy)
print(f"Accuracy: {accuracy:.4f}")




