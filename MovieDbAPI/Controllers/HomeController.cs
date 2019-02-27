using MovieDbAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private MoviesDB db = new MoviesDB();

        public ActionResult Index()
        {
            Movies m = MoviesDAL.GetPost("");

            return View(m);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            // first test to push file
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult UserRegistration()
        {
            return View();
        }

        public ActionResult SaveUser()
        {
            //need to add the user to the Database(MovieDB) - they will be a seperate table/entity where the Primary Key is user Id and pulls the Movide ID WHEN the user favorites from a search
            return View();
            //return RedirectToAction(MovieSearch)  once their registered we can take them to the search so they can start adding movies
        }

        public ActionResult MovieSearch()
        {
            return View();
        }

        public ActionResult SearchResult(string Search)//this is the random search from the user
        {
            List<Movies> foundMovies = new List<Movies>();
            //Change the i= to s= to do a seach (this will get a different data structure and multiple movies
            HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?s=" + Search + "&apikey=459c139");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            JObject MoviesJson = JObject.Parse(data);
            if (MoviesJson["Response"].ToString() == "True")
            {
                for (int i = 0; i < MoviesJson["Search"].Count(); i++)
                {
                    if (MoviesJson["Search"][i]["Type"].ToString() == "movie")
                    {
                        string title = MoviesJson["Search"][i]["Title"].ToString();
                        string year = MoviesJson["Search"][i]["Year"].ToString();
                        string poster = MoviesJson["Search"][i]["Poster"].ToString();
                        Movies m = new Movies("", title, "", year, "", "", "", "", poster);
                        foundMovies.Add(m);
                    }

                }
                ViewBag.Results = foundMovies;
                return View();
            }
            else
            {

                ViewBag.ErrorMessage = MoviesJson["Error"].ToString();
                return View("MovieSearch");
            }

        }

        public ActionResult MoviesDB(string Title)//exact title search and display of the data after the item from the list is clicked on
        {
            List<Movies> movies = new List<Movies>();

            HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?t=" + Title + "&apikey=459c139");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            JObject MoviesJson = JObject.Parse(data);
            try
            {
                if (MoviesJson["Title"].ToString() != null && MoviesJson["Type"].ToString() == "movie")
                {
                    string movieID = MoviesJson["imdbID"].ToString();
                    string title = MoviesJson["Title"].ToString();
                    string genre = MoviesJson["Genre"].ToString();
                    string year = MoviesJson["Year"].ToString();
                    string synopsis = MoviesJson["Plot"].ToString();
                    string director = MoviesJson["Director"].ToString();
                    string rating = MoviesJson["Metascore"].ToString();
                    string mpRating = MoviesJson["Rated"].ToString();
                    string poster = MoviesJson["Poster"].ToString();

                    Movies m = new Movies(movieID, title, genre, year, synopsis, director, rating, mpRating, poster);
                    movies.Add(m);
                    Session["m"] = m;
                    //Movies checkMovieID = db.Movie.Find(movieID);
                    //if(checkMovieID.MovieID == movieID)
                    //{
                    //    ViewBag.Message = "This film is already in the database.";
                    //}
                    return View(movies);
                }
                else
                {
                    ViewBag.ErrorMessage = "Could not find any results. Please try again.";
                    return View("MovieSearch");
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Could not find any results. Please try again.";
                return View("MovieSearch");
            }
        }
    }
}