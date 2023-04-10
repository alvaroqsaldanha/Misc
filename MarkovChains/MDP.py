
# Activity 1


def load_mdp(filename, g):
    MDP = np.load(filename)
    return tuple(MDP['X']),tuple(MDP['A']),tuple(MDP['P']),MDP['c'],g

# Activity 2

def noisy_policy(MDP,a,eps):
    Pi = np.zeros((len(MDP[0]),len(MDP[1])))
    Pi[:,:] = eps / (len(MDP[1]) - 1)
    Pi[:,a] = 1-eps
    return Pi

# Activity 3

def evaluate_pol(MDP,Pi):
    P_Pi = np.zeros((len(MDP[0]),len(MDP[0])))
    C_Pi = np.sum(np.multiply(Pi,MDP[3]),axis=1)
    for i in range(len(MDP[1])):
        P_Pi += np.matmul(np.diag(Pi[:, i]), MDP[2][i])
    J_Pi = np.linalg.inv(np.eye(len(MDP[0])) - MDP[4] * P_Pi)
    return np.reshape(np.matmul(J_Pi,C_Pi),(len(MDP[0]),1))

# Activity 4

def value_iteration(MDP):
    e = 1e-8
    eps = 1.0
    J = np.zeros((len(MDP[0]),1))
    t1 = time.time()
    iteration = 0
    while eps > e:
        Q = np.zeros((len(MDP[0]),len(MDP[1])))
        for a in range(len(MDP[1])):
            Q[:,a,None] = MDP[3][:,a,None] + MDP[4] * MDP[2][a].dot(J)
        Jnew = np.min(Q,axis=1,keepdims=True)
        eps = np.linalg.norm(J-Jnew)
        iteration += 1
        J = Jnew
    t2 = time.time()
    print('Execution time: ' + str(np.round(t2-t1,3)) + ' seconds')
    print("N. Iterations: ", iteration)
    return J

# Activity 5

def policy_iteration(MDP):
    t1 = time.time()
    iteration = 0
    quit = False
    Pi = np.ones((len(MDP[0]),len(MDP[1])))
    while not quit:
        Q = np.zeros((len(MDP[0]),len(MDP[1])))
        CPI = np.sum(MDP[3] * Pi,axis=1,keepdims=True)
        PPI = Pi[:,0,None] * MDP[2][0]
        
        for a in range(1,len(MDP[1])):
            PPI += Pi[:,a,None] * MDP[2][a]
            
        J_Pi = np.linalg.inv(np.eye(len(MDP[0])) - MDP[4] * PPI).dot(CPI)
        
        for a in range(len(MDP[1])):
            Q[:,a,None] = MDP[3][:,a,None] + MDP[4] * MDP[2][a].dot(J_Pi)
        
        Qmin = np.min(Q,axis=1,keepdims=True)
        
        Pinew = np.isclose(Q,Qmin,atol=1e-8,rtol=1e-8).astype(int)
        Pinorm = Pinew / Pinew.sum(axis=1,keepdims=True)
        quit = (Pi == Pinorm).all()
        
        Pi = Pinorm
        iteration += 1
        
    t2 = time.time()
    print('Execution time: ' + str(np.round(t2-t1,3)) + ' seconds')
    print("N. Iterations: ", iteration)
    return np.round(Pi,3)

NRUNS = 100 # Do not delete this
import numpy as np
M = load_mdp('garbage-big.npz', 0.99)
import time

# Activity 6

def simulate(MDP,Pi,x0,length):
    for run in range(NRUNS):
        cost = 0
        for i in range(length):
            a = np.random.choice(len(MDP[1]),p=Pi[x0,:])
            cost += MDP[4] ** i * MDP[3][x0,a]
            x0 = np.random.choice(len(MDP[0]),p=MDP[2][a][x0,:])
    return (cost / NRUNS) * 100


