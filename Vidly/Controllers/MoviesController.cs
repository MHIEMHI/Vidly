

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies 
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View(User.IsInRole(RoleName.CanManageMovies) ? "List" : "ReadOnlyList");
        }

        public ActionResult Details(int Id)
        {
            return View();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFromViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)//check validation (from annotation on the class)
            {
                var viewModel = new MovieFromViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Today;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                // TryUpdateModel(customerInDB); 
                movieInDB.DateAdded = DateTime.Now;
                movieInDB.Genre = movie.Genre;
                movieInDB.NumberInStock = movie.NumberInStock;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.Name = movie.Name;
                //Mapper.Map(movie, movieInDB);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "Movies");
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {

                var viewModel = new MovieFromViewModel(movie)
                { 
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
        }
        /* 
        * public ActionResult Random()
        {
            var movie = new Movie() { Name ="Shrek!" };
            var customers = new List<Customer>()
            {
                new Customer { Name ="John"},
                new Customer {Name = "Mehdi"}

            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers 
            };
            return View(viewModel);
            //return Content("Hello World!!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home",new {page =1,sortBy ="name"});
        }
        */
        /*
        
        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(string.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content(year+" / "+month);
        }
        */

    }
    
}