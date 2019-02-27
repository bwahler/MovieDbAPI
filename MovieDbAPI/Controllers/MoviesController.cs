using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieDbAPI.Models;

namespace MovieDbAPI.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesDB db = new MoviesDB();

        // GET: Movies
        public ActionResult Index()
        {
            //we can make this view the List return after a search or show their favorites
            return View();
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movie.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movie.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }
        public ActionResult Favorites(string movieID)
        {
            Session["movie"] = db.Movie;
            ViewBag.movie = Session["movie"];
            return View();
        }

        public ActionResult Add(Movies movie)
        {
            if (movie.MovieID != null)
            {
                movie = (Movies)Session["m"];
                string movieID = movie.MovieID;
                
                db.Movie.Add(movie);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception)
                {

                    ViewBag.ErrorMessage = "This film is already in the database.";
                    ViewBag.movie = db.Movie;
                    return View("Favorites");
                }
            }
            return RedirectToAction("Favorites");

           
        }


        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,Title,Genre,Year,Synopsis,Director,Rating,MPRating")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }
                
        public ActionResult Delete(string movieID)
        {
            Movies movie = db.Movie.Find(movieID);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Favorites");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}