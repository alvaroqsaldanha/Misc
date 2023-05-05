import torch
import torch.nn as nn
import torch.optim as optim
import torchvision.datasets as datasets
import torchvision.transforms as transforms
from torch.utils.data import DataLoader 
from torchvision.models import resnet34
from torchsummary import summary
import ssl
ssl._create_default_https_context = ssl._create_unverified_context
BATCHSIZE = 64
NUM_EPOCHS = 10
NUM_CLASSES = 100

model = resnet34(pretrained=True)
print(model)
summary(model, input_size=(3, 224, 224))

device = "cpu"
if torch.cuda.is_available():
    device = "cuda"

train_transform = transforms.Compose([
    transforms.Resize((227,227)),
    transforms.RandomVerticalFlip(),
    transforms.ToTensor(),
    transforms.Normalize((0.5,0.5,0.5),(0.5,0.5,0.5))
])

test_transform = transforms.Compose([
    transforms.Resize((224,224)),
    transforms.ToTensor(),
    transforms.Normalize((0.5,0.5,0.5),(0.5,0.5,0.5))
])

trainset = datasets.CIFAR100(root='./data',train=True,transform=train_transform,download=True)
trainset, valset = torch.utils.data.random_split(trainset,[0.8,0.2])
testset = datasets.CIFAR100(root='./data',train=False,transform=test_transform,download=True)

train_loader = DataLoader(trainset,batch_size=BATCHSIZE,shuffle=True)
val_loader = DataLoader(valset,batch_size=BATCHSIZE,shuffle=True)
test_loader = DataLoader(testset,batch_size=BATCHSIZE,shuffle=False)

class AlexNet(nn.Module):
    def __init__(self,num_classes,input_channels):
        super(AlexNet,self).__init__()
        self.conv = nn.Sequential(
            nn.Conv2d(in_channels=input_channels,out_channels=96,kernel_size=11,stride=4),
            nn.BatchNorm2d(96),
            nn.ReLU(),
            nn.MaxPool2d(kernel_size=3,stride=2),
            nn.Conv2d(in_channels=96,out_channels=256,kernel_size=5,padding=2),
            nn.BatchNorm2d(256),
            nn.ReLU(),
            nn.MaxPool2d(kernel_size=3,stride=2),
            nn.Conv2d(in_channels=256,out_channels=384,kernel_size=3,padding=1),
            nn.BatchNorm2d(384),
            nn.ReLU(),
            nn.Conv2d(in_channels=384,out_channels=384,kernel_size=3,padding=1),
            nn.BatchNorm2d(384),
            nn.ReLU(),
            nn.Conv2d(in_channels=384,out_channels=256,kernel_size=3,padding=1),
            nn.BatchNorm2d(256),
            nn.ReLU(),
            nn.MaxPool2d(3,3)
        )
        self.fc = nn.Sequential(
            nn.Linear(4*4*256,4096),
            nn.ReLU(),
            nn.Dropout(p=0.5),
            nn.Linear(4096,1000),
            nn.ReLU(),
            nn.Dropout(p=0.5),
            nn.Linear(1000,num_classes),
        )

    def forward(self,x):
        out = self.conv(x)
        out = out.view(-1,256*4*4)
        return self.fc(out)
    
class FirstBlock(nn.Module):
    def __init__(self,input_channels,output_channels):
        super(FirstBlock,self).__init__()
        self.block = nn.Sequential(
            nn.Conv2d(input_channels,output_channels,kernel_size=3,padding=1),
            nn.ReLU(),
            nn.Conv2d(output_channels,output_channels,kernel_size=3,padding=1),
            nn.ReLU(),
            nn.MaxPool2d(2,2)
        )

    def forward(self,x):
        return self.block(x)
    
class SecondBlock(nn.Module):
    def __init__(self,input_channels,output_channels):
        super(SecondBlock,self).__init__()
        self.block = nn.Sequential(
            nn.Conv2d(input_channels,output_channels,kernel_size=3,padding=1),
            nn.ReLU(),
            nn.Conv2d(output_channels,output_channels,kernel_size=3,padding=1),
            nn.ReLU(),
            nn.Conv2d(output_channels,output_channels,kernel_size=3,padding=1),
            nn.ReLU(),
            nn.MaxPool2d(2,2)
        )

    def forward(self,x):
        return self.block(x)
    
class VGG16(nn.Module):
    def __init__(self,num_channels,num_classes):
        super(VGG16,self).__init__()
        self.first = nn.Sequential(
            FirstBlock(num_channels,64),
            FirstBlock(64,128)
        )
        self.second = nn.Sequential(
            SecondBlock(128,256),
            SecondBlock(256,512),
            SecondBlock(512,512)
        )
        self.fc = nn.Sequential(
            nn.Linear(512*7*7,4096),
            nn.ReLU(),
            nn.Linear(4096,4096),
            nn.ReLU(),
            nn.Linear(4096,num_classes)
        )
        self.avgpool = nn.AdaptiveAvgPool2d((7, 7))

    def forward(self,x):
        out = self.first(x)
        out = self.second(out)
        out = self.avgpool(out)
        out = out.view(-1,512*7*7)
        return self.fc(out)

model = VGG16(num_channels=3,num_classes=100).to(device)
loss = nn.CrossEntropyLoss()
optimizer = optim.Adam(model.parameters(),lr=0.01)

model.train()
accuracy = 0
for epoch in range(NUM_EPOCHS):
    train_loss, train_correct = 0,0
    for i, (images,labels) in enumerate(train_loader):
        print(f"Batch {i}")
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

val_loss, val_correct = 0,0
for images, labels in val_loader:
    images = images.to(device)
    labels = labels.to(device)
    outputs = model(images)
    curr_loss = loss(outputs,labels)
    val_loss += curr_loss.item() * images.size(0)
    val_correct += (outputs.argmax(dim=1) == labels).sum().item()

val_loss /= len(val_loader.dataset)
val_acc = val_correct / len(val_loader.dataset)
