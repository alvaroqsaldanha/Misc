import torch
import torch.nn as nn
import torchvision.transforms as transforms
import pandas as pd
from torch.utils.data import Dataset, DataLoader
import torchaudio
import os

class UrbanAudioDataset(Dataset):

    def __init__(self,directory_path,file):
        self.directory_path = directory_path
        self.annotations = pd.read_csv(file)

    def __len__(self):
        return len(self.annotations)
    
    def __getitem__(self, index) -> Any:
        audio_sample_path = self.get_sample_path(index)
        label = self.get_sample_label(index)

    def get_sample_path(self,index):
        folder = f"fold{self.annotations.iloc[index,5]}"
        path = os.path.join(self.directory_path,folder,self.annotations.iloc[index,0])
        return path
    
    def get_sample_label(self,index):
        return self.annotations.iloc[index,6]
