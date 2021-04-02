import matplotlib.pyplot as plt
import numpy as np
from mpl_toolkits.mplot3d import Axes3D
import pandas as pd
from scipy.interpolate import griddata

colnames=['x', 'y', 'z']
df = pd.read_csv("out.txt", sep='\s+', header=None, names=colnames)

x1 = np.linspace(df['x'].min(), df['x'].max(), len(df['x'].unique()))
y1 = np.linspace(df['y'].min(), df['y'].max(), len(df['y'].unique()))
x2, y2 = np.meshgrid(x1, y1)
z2 = griddata((df['x'], df['y']), df['z'], (x2, y2), method='cubic')

fig = plt.figure()
ax = plt.axes(projection='3d')

surf = ax.plot_surface(x2, y2, z2, rstride=1, cstride=1, cmap='viridis', edgecolor='none')

ax.set_title('Ez = x**2 + y**2');

ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Ez')

#plt.show()
plt.savefig('saved_figure.png')