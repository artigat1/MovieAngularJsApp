using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using MovieAngularJsApp.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieAngularJsApp.API
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly MoviesAppContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesController"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public MoviesController(MoviesAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _dbContext.Movies;
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if(movie == null)
            {
                return new HttpNotFoundResult();
            }

            return new ObjectResult(movie);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.Id == 0)
                {
                    _dbContext.Movies.Add(movie);
                    _dbContext.SaveChanges();
                    return new ObjectResult(movie);
                }
                else
                {
                    var original = _dbContext.Movies.FirstOrDefault(m => m.Id == movie.Id);
                    original.Title = movie.Title;
                    original.Director = movie.Director;
                    original.TicketPrice = movie.TicketPrice;
                    original.ReleaseDate = movie.ReleaseDate;
                    _dbContext.SaveChanges();
                    return new ObjectResult(original);
                }
            }

            return new BadRequestObjectResult(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            return new HttpStatusCodeResult(200);
        }
    }
}
