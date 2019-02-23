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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MoviesDB()
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://www.omdbapi.com/?i=tt3896198&apikey=459c139");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            JObject MoviesJson = JObject.Parse(data);

            List<JToken> movie = MoviesJson["Type"].ToList();

            List<Movies> output = new List<Movies>();
            for (int i = 0; i < MoviesJson.Count; i++)
            {
                Movies m = new Movies();

                m.Title = movie[i]["Title"].ToString();
                //m.ImageURL = movie[i]["data"]["thumbnail"].ToString();
                //m.LinkURL = "http://www.omdbapi.com/" + movie[i]["data"]["permalink"].ToString();
                output.Add(m);
            }

            return View(output);
        }
    }
}