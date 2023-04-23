import torch
import torch.nn as nn
import numpy as np
import torchvision.transforms as transforms
import torchvision.datasets as datasets
from torch.utils.data import DataLoader, Dataset

BATCHSIZE = 64

train_transform = transforms.Compose([
    transforms.RandomCrop(32, padding=4),
    transforms.RandomHorizontalFlip(),
    transforms.ToTensor(),
    transforms.Normalize((0.5071, 0.4865, 0.4409), (0.2673, 0.2564, 0.2762))
])

test_transform = transforms.Compose([
    transforms.ToTensor(),
    transforms.Normalize((0.5071, 0.4865, 0.4409), (0.2673, 0.2564, 0.2762))
])

device = 'cpu'
if torch.cuda.is_available():
    device = 'cuda'

train_set = datasets.CIFAR100(root='./data',train=True,download=True,transform=train_transform)
train_set, val_set = torch.utils.data.random_split(train_set,[0.8,0.2])
test_set = datasets.CIFAR100(root='./data',train=False,download=True,transform=test_transform)
train_loader = DataLoader(train_set,batch_size=BATCHSIZE,shuffle=True)
val_loader = DataLoader(val_set,batch_size=BATCHSIZE,shuffle=True)
test_loader = DataLoader(test_set,batch_size=BATCHSIZE,shuffle=False)

class CNN(nn.Module):
    def __init__(self,num_classes, input_size):
        super(CNN,self).__init__()
        self.conv = nn.Sequential(
            nn.Conv2d(in_channels=input_size,out_channels=32,kernel_size=3,padding=1,stride=1),
            nn.ReLU(),
            nn.Conv2d(in_channels=32,out_channels=64,kernel_size=3,padding=1,stride=1),
            nn.MaxPool2d(2,2),
            nn.BatchNorm2d(64),
            nn.ReLU(),
            nn.Conv2d(in_channels=64,out_channels=128,kernel_size=3,padding=1,stride=1),
            nn.MaxPool2d(2,2),
            nn.BatchNorm2d(128),
            nn.ReLU()
        )
        self.fc = nn.Sequential(
            nn.Linear(128*8*8,512),
            nn.ReLU(),
            nn.Linear(512,num_classes)
        )

    def forward(self,x):
        out = self.conv(x)
        out = out.view(-1,128*8*8)
        out = self.fc(out)
        return out

model = CNN(100,3).to(device)
learning_rate = 0.01
loss = nn.CrossEntropyLoss()
optimizer = torch.optim.SGD(model.parameters(),lr=learning_rate,momentum=0.9,weight_decay=5e-4)
scheduler = torch.optim.lr_scheduler.MultiStepLR(optimizer, milestones=[80, 120], gamma=0.1)

num_epochs = 10

model.train()

for epoch in range(num_epochs):
    train_loss = 0
    train_correct = 0
    for i, (images,labels) in enumerate(train_loader):
        images.to(device)
        labels.to(device)
        outputs = model(images)
        curr_loss = loss(outputs,labels)
        curr_loss.backward()
        optimizer.step()
        optimizer.zero_grad()

        train_loss += curr_loss.item() * images.size(0)
        train_correct += (outputs.argmax(dim=1) == labels).sum().item()

    train_loss /= len(train_loader.dataset)
    train_acc = train_correct / len(train_loader.dataset)

    print('Train Loss: {:.4f}, Train Acc: {:.4f}'.format(train_loss, train_acc))

model.eval()

val_loss = 0.0
val_correct = 0

with torch.no_grad():
    for inputs, targets in val_loader:
        outputs = model(inputs)
        cur_loss = loss(outputs, targets)
        
        val_loss += curr_loss.item() * inputs.size(0)
        val_correct += (outputs.argmax(dim=1) == targets).sum().item()

val_loss /= len(val_loader.dataset)
val_acc = val_correct / len(val_loader.dataset)
scheduler.step()

model.eval()
test_loss = 0.0
test_correct = 0

with torch.no_grad():
    for inputs, targets in test_loader:
        outputs = model(inputs)
        cur_loss = loss(outputs, targets)

        test_loss += curr_loss.item() * inputs.size(0)
        test_correct += (outputs.argmax(dim=1) == targets).sum().item()
    test_loss /= len(test_loader.dataset)
    test_acc = test_correct / len(test_loader.dataset)

print('Test Loss: {:.4f}, Test Acc: {:.4f}'.format(test_loss, test_acc))




