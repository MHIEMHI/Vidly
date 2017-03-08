using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }


        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            //if(rentalDto.MovieIds.Count ==0)
            //    return BadRequest("No Movie have been selected");

            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);

            //if (customer == null)
            //    return BadRequest("Customer Is not valid.");

            var movies = _context.Movies.Where(
                m=> rentalDto.MovieIds.Contains(m.Id)  /*WHERE Id IN (1,2,3)*/ ).ToList();
            
            //if (movies.Count != rentalDto.MovieIds.Count)
            //    return BadRequest("One or More movie Id's are invalid");

            foreach (var movie in movies)
            {
                //var movie = _context.Movies.Single(m => m.Id == movieId);
                if (movie.NumberAvailable ==  0)
                    return BadRequest("A Movie is not available");

                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
                    
                

            }
            _context.SaveChanges();

            return Ok();
        }

    }
}
