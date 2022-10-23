# IS UNIQUE
def isunique(str):
    checker = 0
    for i in range(len(str)):
        if checker & (1 << ord(str[i])) > 0:
            return False
        checker += (1 << ord(str[i]))
    return True 

