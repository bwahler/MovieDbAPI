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
            Movies rp = MoviesDAL.GetPost(0);

            return View(rp);
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

        public ActionResult MoviesDB()
        {
            List<JToken> movie = new List<JToken>();
            List<string> imdbID = new List<string>();//this will need to be moved over to the Movie object
            {
                imdbID.Add("tt0111161");
                imdbID.Add("tt3896198");

            }
            for (int i=0; i<=imdbID.Count; i++)
            {
                HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?i="+imdbID[i]+"&apikey=459c139");
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
               // movie.Add(title, genre, year, synopsis, director, rating, mpRating);//change to data type to require certain data)

            }   

            
           
            List<Movies> output = new List<Movies>();
            for (int i = 0; i < movie.Count; i++)
            {
                Movies m = new Movies();

                m.Title = movie[i].ToString();
                //m.ImageURL = movie[i]["data"]["thumbnail"].ToString();
                //m.LinkURL = "http://www.omdbapi.com/" + movie[i]["data"]["permalink"].ToString();
                output.Add(m);
            }

            return View(output);
        }
    }
}