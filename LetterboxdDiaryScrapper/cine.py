
import webbrowser

url_list = [
    "http://wasedashochiku.co.jp/",
    "https://www.shin-bungeiza.com/schedule",
    "https://www.imageforum.co.jp/theatre/movies/",
    "https://www.cinemart.co.jp/theater/shinjuku/movie/",
    "https://www.unitedcinemas.jp/ygc/film.php",
    "https://cinemacity.co.jp/",
    "https://www.cinequinto.com/",
    "https://ttcg.jp/cinelibre_ikebukuro/movie/",
    "https://www.bunkamura.co.jp/pickup/movie.html",
    "http://www.okura-movie.co.jp/meguro_cinema/now_showing.html#coming_soon",
    "https://ttcg.jp/human_shibuya/movie/",
    "http://www.cinemavera.com/programs.html",
    "https://shinjuku.musashino-k.jp/movies/",
    "https://tjoy.jp/shinjuku_wald9",
    "http://shimotakaidocinema.com/schedule/schedule.html",
    "https://demachiza.com/movies/movies-type/current",
    "https://ttcg.jp/human_yurakucho/",
    "https://chupki.jpn.org/#screen-area",
    "http://www.eurospace.co.jp/",
    "https://pole2.co.jp/",
    "https://www.ks-cinema.com/movie/",
    "https://tollywood.jp/showing",
    "https://www.unitedcinemas.jp/ygc/film.php"
]

for url in url_list:
    webbrowser.open_new_tab(url)
