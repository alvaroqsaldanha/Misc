

import numpy as np

def load_pomdp(filename, g):
    pomdp = np.load(filename)
    return tuple(pomdp["X"]), tuple(pomdp["A"]), tuple(pomdp["Z"]), tuple(pomdp["P"]), tuple(pomdp["O"]), pomdp["c"], g

import numpy.random as rand

M = load_pomdp('garbage-big.npz', 0.99)

# Activity 2

def gen_trajectory(pomdp,x0,n):
    states = [x0]
    actions = []
    observations = []
    state = x0
    for i in range(n):
        curr_action = rand.choice(len(pomdp[1]))
        actions.append(curr_action)
        state = rand.choice(len(pomdp[0]),p=pomdp[3][curr_action][state,:])
        states.append(state)
        observation = rand.choice(len(pomdp[2]),p=pomdp[4][curr_action][state,:])
        observations.append(observation)
    return np.array(states), np.array(actions), np.array(observations)



# Activity 3

def belief_update(pomdp,curr_belief,action,obs):
    new_belief = curr_belief.dot(pomdp[3][action]).dot(np.diag(pomdp[4][a][:, obs]))
    return new_belief / new_belief.sum()

def sample_beliefs(pomdp,n):
    trajectory = gen_trajectory(pomdp,rand.choice(len(pomdp[0])),n)
    curr_belief = np.ones((1, len(pomdp[0]))) / len(pomdp[0])
    beliefs = [curr_belief]
    for i in range(n):
        curr_belief = belief_update(pomdp,curr_belief,trajectory[1][i],trajectory[2][i])
        if all(np.linalg.norm(b-curr_belief) > 1e-3 for b in beliefs):
            beliefs.append(curr_belief)
    return tuple(beliefs)
    
# Activity 4

def solve_mdp(pomdp):
    Q = np.zeros((len(pomdp[0]), len(pomdp[1])))
    curr_error = 1
    while 1e-8 < curr_error:
        prevQ = np.array(Q, copy=True)
        minQ = np.min(Q, axis=1, keepdims=True)
        for action in range(len(pomdp[1])):
            Q[:, action, None] = pomdp[5][:, action, None] + pomdp[6] * np.matmul(pomdp[3][action], minQ)
        curr_error = np.linalg.norm(Q - prevQ)
    return Q

# Activity 5
def get_heuristic_action(b, Q, h):
    Qmin = np.min(Q, axis=1, keepdims=True)
    Pi = np.isclose(Q, Qmin, atol=1e-8, rtol=1e-8).astype(int) 
    Pi = Pi / np.sum(Pi, axis=1, keepdims=True)
    if h == "mls":
        return rand.choice(Q.shape[1], p=Pi[rand.choice(np.where(b == b.max())[0]),:])
    elif h == "av":
        return rand.choice(np.flatnonzero(b.dot(Pi) == b.dot(Pi).max()))
    elif h == "q-mdp":
        return rand.choice(np.flatnonzero(b.dot(Q) == b.dot(Q).min()))


# Activity 6
 
def solve_fib(pomdp):
    Q = np.zeros((len(pomdp[0]), len(pomdp[1])))
    curr_error = 1
    while curr_error > 1e-1:
        Q_prev = Q.copy()
        Q = np.zeros((len(pomdp[0]), len(pomdp[1])))
        for a in range(len(pomdp[1])):
            for z in range(len(pomdp[2])):
                Q[:, a] = np.min((pomdp[3][a]*pomdp[4][a][:,z]).dot(Q_prev),axis= 1) 
        Q = pomdp[5] + pomdp[6] * Q
        curr_error = np.linalg.norm(Q-Q_prev)
    return Q
 
# All the beliefs are approximately uniform, which means that there is no strong preference for any particular state.
# In most cases, the agents take the same action, which is moving down, regardless of the heuristic. This suggests that in this case, the chosen heuristic should not impact
# performance significantly.



