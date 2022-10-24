# IS UNIQUE
def isunique(str):
    checker = 0
    for i in range(len(str)):
        if checker & (1 << ord(str[i])) > 0:
            return False
        checker += (1 << ord(str[i]))
    return True 

# CHECK PERMUTATION
def checkpermutation(str1,str2):
    if len(str1) != len(str2):
        return False
    char_counter = [0 for x in range(128)]
    for c1 in str1:
        char_counter[ord(c1)] += 1
    for c2 in str2:
        char_counter[ord(c2)] -= 1
        if char_counter[ord(c2)] < 0:
            return False
    return True

# URLify
def urlify(str,l):
    str = list(str)
    curr_replace = len(str) - 1
    for i in range(l-1,-1,-1):
        if str[i] != ' ':
            str[curr_replace] = str[i]
            curr_replace -= 1
        else:
            str[curr_replace] = '0'
            str[curr_replace-1] = '2'
            str[curr_replace-2] = '%'
            curr_replace -= 3
    print(''.join(str))



