import numpy as np
import numpy.random as rnd
import matplotlib.pyplot as plt

# Loads chain, as a serializes Numpy object, returning a tuple (State Space, Transition Probability Matrix).
# States go from 0 to len(P) - 1.
def load_chain(filename):
    P = np.load(filename)
    states = []
    for i in range(0,P.shape[0]):
        states.append(str(i))
    return (tuple(states),P)

# Given a Markov Chain, as described in load_chain, and a trajectory, calculate the trajectories probability.
def prob_trajectory(M,trajectory):
    curr_state = M[0].index(trajectory[0])
    p = 1
    for i in range(1,len(trajectory)):
        next_state = M[0].index(trajectory[i])
        p *= M[1][curr_state,next_state]
        curr_state = next_state
    return p

# Calculate the stationary distribution of the markov chain (assuming it exists, the chain is ergodic).
def stationary_dist(M):
    eigen = np.linalg.eig(M[1].T)
    distribution = eigen[1][:,np.isclose(eigen[0],1)][:,0]
    stationary_distribution = np.real(distribution / sum(distribution))
    return stationary_distribution

# Given a starting distribution, get the distribution after N steps of the Markov chain.
def compute_dist(M,distribution,N):
    M_N = np.linalg.matrix_power(M[1],N)
    return np.matmul(distribution,M_N)

# Get most likely trajectory given a distribution and a Markov chain.
def simulate(M,distribution,N):
    state = rnd.choice(len(M[0]),p=distribution[0])
    trajectory = []
    trajectory.append(M[0][state])
    for i in range(1,N):
        state = rnd.choice(len(M[0]),p=M[1][state])
        trajectory.append(M[0][state])
    return tuple(trajectory)

# Enter the path to your probability matrix file, as described in the first function.
file = ""
M = load_chain(file)
u_star = stationary_dist(M)

steps = 50000

trajectory = simulate(M,np.ones((1,len(M[0]))) / len(M[0]),steps)
idxs = list(map(lambda n: M[0].index(n), trajectory))

plt.figure()
plt.hist(idxs,len(M[0]),density=True,histtype='bar',edgecolor='black')
plt.plot(np.arange(len(M[0])),u_star,'.')
plt.title('Truck State Frequency')
plt.show()

