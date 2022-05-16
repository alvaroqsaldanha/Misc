#Machine Learning Homework #2 Exercise 2 - Alvaro Saldanha 2022917008

import pandas as pd
import io
import numpy as np
import matplotlib.pyplot as plt
from sklearn import svm 

"""a)"""

# Reading the csv files.
recordsx1 = pd.read_csv('hw2_2_X_1.csv')
labelsx1 = pd.read_csv('hw2_2_y_1.csv')

# Initializing variables.
recordsx1.columns = ['X','Y']
colorslbl1 = ['blue' if row[0] == 0 else 'yellow' for index, row in labelsx1.iterrows()]
X, Y = recordsx1['X'], recordsx1['Y']
h = .02
x_min, x_max = X.min() - 1, X.max() + 1
y_min, y_max = Y.min() - 1, Y.max() + 1
xx, yy = np.meshgrid(np.arange(x_min, x_max, h), np.arange(y_min, y_max, h))
xy = np.vstack([xx.ravel(), yy.ravel()]).T

# Training the model.
clf = svm.SVC(kernel='linear')
clf.fit(recordsx1.values, labelsx1.values.ravel())
Z = clf.decision_function(xy).reshape(xx.shape)

# Plotting the data.
plt.scatter(X,Y, color=colorslbl1)
plt.contour(xx, yy, Z, colors='k', levels=[-1, 0, 1], alpha=0.5, linestyles=['--', '-', '--'])
plt.scatter(clf.support_vectors_[:, 0], clf.support_vectors_[:, 1], s=300, linewidth=1, facecolors='none', edgecolors='r')
plt.show()

"""b)"""

# Reading the csv files.
recordsx2 = pd.read_csv('hw2_2_X_2.csv')
labelsx2 = pd.read_csv('hw2_2_y_2.csv')

# Initializing variables.
recordsx2.columns = ['X','Y']
colorslbl2 = ['blue' if row[0] == -1 else 'yellow' for index, row in labelsx2.iterrows()]
X2, Y2 = recordsx2['X'], recordsx2['Y']
h = .02
x2_min, x2_max = X2.min() - 1, X2.max() + 1
y2_min, y2_max = Y2.min() - 1, Y2.max() + 1
xx2, yy2 = np.meshgrid(np.arange(x2_min, x2_max, h), np.arange(y2_min, y2_max, h))
xy2 = np.vstack([xx2.ravel(), yy2.ravel()]).T

# Training the model.
clf_ = svm.SVC(kernel='rbf')
clf_.fit(recordsx2.values, labelsx2.values.ravel())
Z2 = clf_.decision_function(xy2).reshape(xx2.shape)

# Plotting the data.
plt.scatter(X2,Y2, color=colorslbl2)
plt.contour(xx2, yy2, Z2, colors='k', levels=[-1, 0, 1], alpha=0.5, linestyles=['--', '-', '--'])
plt.scatter(clf_.support_vectors_[:, 0], clf_.support_vectors_[:, 1], s=300, linewidth=1, facecolors='none', edgecolors='r')
plt.show()

"""c)"""

# Reading the csv files.
recordsx3 = pd.read_csv('hw2_2_X_3.csv')
labelsx3 = pd.read_csv('hw2_2_y_3.csv')

# Initializing variables.
recordsx3.columns = ['X','Y']
colorslbl3 = ['blue' if row[0] == -1 else 'yellow' for index, row in labelsx3.iterrows()]
X3, Y3 = recordsx3['X'], recordsx3['Y']
h = .02
x3_min, x3_max = X3.min() - 1, X3.max() + 1
y3_min, y3_max = Y3.min() - 1, Y3.max() + 1
xx3, yy3 = np.meshgrid(np.arange(x3_min, x3_max, h), np.arange(y3_min, y3_max, h))
xy3 = np.vstack([xx3.ravel(), yy3.ravel()]).T
C = [0.01, 0.1, 1, 10, 50]

# Training the model and plotting the data for each C.
for c in C:
  clf__ = svm.SVC(kernel='linear', C=c).fit(recordsx3.values, labelsx3.values.ravel())
  clf__.fit(recordsx3.values, labelsx3.values.ravel())
  Z3 = clf__.decision_function(xy3).reshape(xx3.shape)

  plt.scatter(X3,Y3, color=colorslbl3)
  plt.contour(xx3, yy3, Z3, colors='k', levels=[-1, 0, 1], alpha=0.5, linestyles=['--', '-', '--'])
  plt.scatter(clf__.support_vectors_[:, 0], clf__.support_vectors_[:, 1], s=300, linewidth=1, facecolors='none', edgecolors='r')
  plt.show()