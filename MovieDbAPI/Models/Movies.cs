using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MovieDbAPI.Models
{
    public class Movies
    {
        private string output;

        [Key]
        public string MovieID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public string MPRating { get; set; }
        public string Poster { get; set; }
        public string UserIDs { get; set; }

        public Movies()
        {

        }
        public Movies(string movieID, string title, string genre, string year, string synopsis, string director, string rating, string mpRating, string poster)
        {
            MovieID = movieID;
            Title = title;
            Genre = genre;
            Year = int.Parse(year);
            Synopsis = synopsis;
            Director = director;
            Rating = rating;
            MPRating = mpRating;
            Poster = poster;
        }

        public Movies(string APIText)
        {
            var movieJson = JObject.Parse(APIText).ToString();

            JavaScriptSerializer oJS = new JavaScriptSerializer();
            Movies mov = new Movies();

            mov = oJS.Deserialize<Movies>(movieJson);

            Title = mov.Title;
            Year = mov.Year;
            MovieID = mov.MovieID;
            Poster = mov.Poster;
            Genre = mov.Genre;
            MPRating = mov.MPRating;
            Synopsis = mov.Synopsis;
        }


    }
    public class MoviesDB : DbContext
    {
        public DbSet<Movies> Movie { get; set; }
    }

}