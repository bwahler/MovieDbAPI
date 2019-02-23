using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieDbAPI.Models
{
    public class Movies
    {
        [Key]
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public string MPRating { get; set; }
    }

    public class MoviesDB : DbContext
    {
        public DbSet<Movies> Movie { get; set; }
    }

    //public Movies(string title, string genre, string year, string synopsis, string director, string rating, string mpRating)
    //{
    //    Title=title; genre, year, synopsis, director, rating, mpRating
    //}

}