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
        public ActionResult Index()
        {
            Movies m = MoviesDAL.GetPost(0);

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
            //the code is broken anyways do to the JOBJECT, but I was not even able to run it earlier, so you are already fixed it without copying the coffeeshop db
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
            //this will be a dousy method. Basics: search and let them Favorite. (should we let them search without being a user??)
            //start out with just searchign by EXACT title (so take in user input - form)
            //then do a contains any type of search????
            //then add another search filter (director, year, rating limiter, etc)
            //best way would store the initial (first entered in) search results into a list and then do a loop through the list and do seperate if statements for each new filter i.e.- if genre!=null{List.ContainsAny(genre) add to another list} 


            return View();
        }

        public ActionResult MoviesDB(string Title)
        {
            List<Movies> movies = new List<Movies>();
            List<string> imdbID = new List<string>();//this will change based on the search results put in (we probably will do some refactoring later)
            {
                imdbID.Add("tt0111161");
                imdbID.Add("tt3896198");

            }
            //t is for title, s is for a search
            //for (int i=0; i<imdbID.Count; i++)
            //{
                //Change the i= to t= to search by title
                //Change the i= to s= to do a seach (this will get a different data structure and multiple movies
                //search probably needs to be its own Action
                HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?t=" + Title + "&apikey=459c139");
                //HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?i="+imdbID[i]+"&apikey=459c139");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader rd = new StreamReader(response.GetResponseStream());
                string data = rd.ReadToEnd();
                JObject MoviesJson = JObject.Parse(data);
                string title = MoviesJson["Title"].ToString();
                string genre = MoviesJson["Genre"].ToString(); //maybe change to a list to allow cross genre searching - we will need to do a loop for each in the model
                string year = MoviesJson["Year"].ToString();
                string synopsis = MoviesJson["Plot"].ToString();
                string director = MoviesJson["Director"].ToString();
                string rating = MoviesJson["Metascore"].ToString();//we can change the rating we are pulling from
                string mpRating = MoviesJson["Rated"].ToString();
                string poster = MoviesJson["Poster"].ToString();

                Movies m = new Movies(title, genre, year, synopsis, director, rating, mpRating, poster);
                movies.Add(m);
                
            //}   

            
           
            //List<Movies> output = new List<Movies>();
            //for (int i = 0; i < output.Count; i++)
            //{
            //    Movies m = new Movies();

            //    m.Title = output[i].ToString();
            //    //m.ImageURL = movie[i]["data"]["thumbnail"].ToString();
            //    //m.LinkURL = "http://www.omdbapi.com/" + movie[i]["data"]["permalink"].ToString();
            //    output.Add(m);
            //}

            return View(movies);
        }

       
    }
}