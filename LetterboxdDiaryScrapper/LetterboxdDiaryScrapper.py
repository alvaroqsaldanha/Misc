import requests
from bs4 import BeautifulSoup 
import os

print("LETTERBOXD DIARY WEB SCRAPPER")
print("Please enter your username: ")
username = input()
page_counter = 1 
film_counter = 0
diary = open("LetterboxdDiary.txt","w+")
print("Loading...")

while 1:
    URL = "https://letterboxd.com/" + username + "/films/diary/page/" + str(page_counter) + "/" 
    page = requests.get(URL) 
    if page.status_code != 200:
        print("Couldn't reach Letterboxd")
        break
    soup = BeautifulSoup(page.content, "html.parser")
    results = soup.findAll("h3", {"class" : "headline-3"}) 
    if len(results) == 0:
        print("All done :)!")
        os.startfile('LetterboxdDiary.txt')
        break 
    film_counter += len(results)
    print("Found " + str(film_counter) + " films up to this point...")
    for x in results:
        children = x.findChildren("a" , recursive=False)
        for child in children:
            diary.write(child.text + '\n') 
    page_counter += 1

diary.close() 





