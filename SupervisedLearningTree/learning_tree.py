import numpy as np
from copy import deepcopy
from math import log2

def entropy(p,n):
    if p == n:
        return 1
    if p == 0 or n == 0:
        return 0
    return - p * log2(p) - n * log2(n)


def getBestAttribute(examples,results,features,noise):
    pExamples = results.count(1)
    nExamples = results.count(0)
    total = len(results)
    tableEntropy = entropy(pExamples / total, nExamples / total)

    back = []
    highestGain = 0
    for x in features:
        nfeaturesp = 0
        pfeaturesp = 0
        nfeaturesn = 0
        pfeaturesn = 0
        for y in range(len(examples)):
            if examples[y][x] == 1:
                if results[y] == 1:
                    pfeaturesp += 1
                else:
                    nfeaturesp += 1
            else:
                if results[y] == 1:
                    pfeaturesn += 1
                else:
                    nfeaturesn += 1

        total = nfeaturesp + pfeaturesp + pfeaturesn + nfeaturesn
        if nfeaturesp + pfeaturesp != 0:
            pentropy = entropy(pfeaturesp / (nfeaturesp + pfeaturesp), nfeaturesp / (nfeaturesp + pfeaturesp))
        else:
            pentropy = 1
        if nfeaturesn + pfeaturesn != 0:
            nentropy = entropy(pfeaturesn / (nfeaturesn + pfeaturesn), nfeaturesn / (nfeaturesn + pfeaturesn))
        else:
            nentropy = 1
        probPositive = (pfeaturesp + nfeaturesp) / total
        probNegative = (pfeaturesn + nfeaturesn) / total

        gain = tableEntropy - (probPositive * pentropy + probNegative * nentropy)
        if gain > highestGain:
            highestGain = gain
        back.append([x,gain])
    r = []
    for i in range(len(back)):
        if back[i][1] == highestGain:
            r.append(back[i][0])
    return r

def most_frequent(List): 
    return max(set(List), key = List.count)

def createdecisiontreeaux(examples,results,features,parent_results,noise):
    if not examples:
        return int(most_frequent(parent_results))
    elif all(x==results[0] for x in results):
        if not parent_results:
            return [0,int(results[0]),int(results[0])]
        else:
            return int(results[0])
    elif not features:
        return int(most_frequent(parent_results))
    else:
        attribute = getBestAttribute(examples, results, features,noise)
        trees = []
        for i in range(len(attribute)):
            tree = [attribute[i]]
            values = [0,1]
            new_features = deepcopy(features)
            new_features.remove(attribute[i])
            for x in values:
                exs = []
                new_results = []
                for y in range(len(examples)):
                    if examples[y][attribute[i]] == x:
                        exs.append(examples[y])
                        new_results.append(results[y])
                subtree = createdecisiontreeaux(exs,new_results,new_features,results,noise)
                tree.append(subtree)
            trees.append(tree)
        minlen = 0
        for x in trees:
            if len(str(x)) < minlen or minlen == 0:
                minlen = len(str(x))
                besttree = x
        return besttree

def postpruning(tree,examples,results,attributes,noise):
    if isinstance(tree,list) and isinstance(tree[1],list) and isinstance(tree[2],list)  and tree[1][0] == tree[2][0]:
        new_features = deepcopy(attributes)
        new_features.remove(tree[1][0])
        besttree = [tree[1][0]]
        values = [0,1]
        for x in values:
            exs = []
            new_results = []
            for y in range(len(examples)):
                if examples[y][tree[1][0]] == x:
                    exs.append(examples[y])
                    new_results.append(results[y])
            subtree = createdecisiontreeaux(exs,new_results,new_features,results,noise)
            besttree.append(subtree)
        tree = besttree
    elif isinstance(tree,list) and not isinstance(tree[1],int) and not isinstance(tree[2],int):
        new_features = deepcopy(attributes)
        new_features.remove(tree[0])
        besttree = [tree[0]]
        values = [0,1]
        for x in values:
            exs = []
            new_results = []
            for y in range(len(examples)):
                if examples[y][tree[0]] == x:
                    exs.append(examples[y])
                    new_results.append(results[y])
            subtree = postpruning(tree[x + 1],exs,new_results,new_features,noise)
            besttree.append(subtree)
        tree = besttree
    return tree

def createdecisiontree(D,Y, noise = False):
    a = D.shape
    A = list(range(a[1]))
    examples = D.tolist()
    results = Y.tolist()
    parent_examples = []
    tree = createdecisiontreeaux(examples,results,A,parent_examples,noise)
    newtree = postpruning(tree,examples,results,A,noise)
    originalLength = len(str(tree))
    newLength = len(str(newtree))
    if originalLength <= newLength:
        besttree = tree
    else:
        besttree = newtree
    return besttree
