# -*- coding: utf-8 -*-
"""
Grupo al023
Student id #92416
Student id #92473
"""

import numpy as np
from copy import deepcopy
from math import log2

def entropy(p,n):
    if p == n:
        return 1
    if p == 0 or n == 0:
        return 0
    return - p * log2(p) - n * log2(n)


def getBestAttribute(examples,results,features):
    pExamples = results.count(1)
    nExamples = results.count(0)
    total = len(results)
    tableEntropy = entropy(pExamples / total, nExamples / total)

    best = features[0]
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
            best = x
        #print("Gain: ", gain, "x:",x)
    return best

def most_frequent(List): 
    return max(set(List), key = List.count)

def createdecisiontreeaux(examples,results,features,parent_results):
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
        attribute = getBestAttribute(examples, results, features)
        #print ("Chosen: ",attribute)
        tree = [attribute]
        values = [0,1]
        new_features = deepcopy(features)
        new_features.remove(attribute)
        for x in values:
            exs = []
            new_results = []
            for y in range(len(examples)):
                if examples[y][attribute] == x:
                    exs.append(examples[y])
                    new_results.append(results[y])
            subtree = createdecisiontreeaux(exs,new_results,new_features,results)
            tree.append(subtree)
        return tree
    

def createdecisiontree(D,Y, noise = False):
    a = D.shape
    A = list(range(a[1]))
    examples = D.tolist()
    results = Y.tolist()
    parent_examples = []
    tree = createdecisiontreeaux(examples,results,A,parent_examples)
    print(len(str(tree)))
    return tree



