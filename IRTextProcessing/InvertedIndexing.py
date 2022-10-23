import sys
from sklearn.feature_extraction.text import CountVectorizer
import nltk
import matplotlib.pyplot as plt
from math import log

wordindex = {}
aux_map = {}
idfs = {}

def dict_print(dicttp):
    for key in dicttp.keys():
        print(key + ": " + str(dicttp[key]))

def idx_stats(wordidx):
    print("Number of documents: " + str(len(sys.argv) - 1))
    print("Number of terms in built word index: " + str(len(wordidx.keys())))
    print("Number of occurences (number of documents the term appears in) by term:")
    for word in wordidx.keys():
        print(word + ": " + str(len(wordidx[word])))

def idf(term):
    N = len(sys.argv) - 1 # total number of documents
    df = len(wordindex[term]) # total number of documents where term appears
    result = log(N/df)
    #print("The idf for the term " + term + " is " + str(result) + ".")
    return result

def store_idfs():
    for term in wordindex.keys():
        idfs[term] = idf(term)

def check_files_with_term(term):
    if wordindex.get(term) != None:
        for tup in wordindex[term]:
            print(tup[0])
    else:
        print("Term " + term + " does not appear in any document.")


for i in range(1,len(sys.argv)):
    filename = sys.argv[i]
    try:
        file = open(filename,"r")
    except FileNotFoundError:
        print("Error processing file " + filename + ".")
        continue
    filebody = file.read()
    vectorizer = CountVectorizer()
    X = vectorizer.fit_transform([filebody])
    fileterms = vectorizer.get_feature_names_out()
    aux_map[filename] = (len(nltk.word_tokenize(filebody)),len(fileterms))
    matrix = X.toarray()[0]
    for word in fileterms:
        if wordindex.get(word) == None:
            wordindex[word] = [(filename,matrix[vectorizer.vocabulary_[word]])]
        elif filename not in wordindex[word]:
            wordindex[word].append((filename,matrix[vectorizer.vocabulary_[word]]))

#dict_print(wordindex)
#idx_stats(wordindex)
store_idfs()

term_count = [aux_map[key][1] for key in aux_map.keys()]
plt.bar(aux_map.keys(), term_count, label="Terms per document")
#plt.show()

val = ""
while val != 'q':
    print("Press:")
    print("1 - To select documents with specific input term")
    print("q - To quit")
    val = input(">>> ")
    if val == "1":
        term = input("Type term: ")
        check_files_with_term(term)
    elif val == 'q':
        exit()











