import torch
import numpy as np
from torch.utils.data import Dataset, DataLoader
import torchvision
import math

class CustomDataset(Dataset):

    def __init__(self, transform=None):
        xy = np.loadtxt('wine.csv',delimiter=',',dtype=np.float32,skiprows=1)
        self.x = xy[:,1:]
        self,y = xy[:,[0]]
        self.n_samples = xy.shape[0]
        self.transform = transform

    def __getitem__(self, index):
        sample = self.x[index], self.y[index]
        if self.transform:
            sample = self.transform(sample)
        return sample
    
    def __len__(self):
        return self.n_samples
    
class ToTensor:
    def __call__(self,sample):
        inputs, targets = sample
        return torch.from_numpy(inputs), torch.from_numpy(targets)
    
class MulT:
    def __init__(self,factor):
        self.factor = factor

    def __call__(self,sample):
        inputs, target = sample
        return inputs * self.factor, target
    
dataset = CustomDataset(transform=ToTensor())
composed = torchvision.transforms.Compose([ToTensor(),MulT(2)])
dataset = CustomDataset(transform=composed)
    
