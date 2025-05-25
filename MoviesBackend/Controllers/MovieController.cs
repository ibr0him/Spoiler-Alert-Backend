using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Context;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        MovContext Db;
        public MovieController(MovContext _Db)
        {
            Db = _Db;
        }

        [HttpGet] //api/Movie
        public IActionResult GetAll()
        {
            var Movies = Db.Movies.ToList();
            
            return Ok(Movies);
        }
        [HttpPost] //api/Movie --> Post

        public IActionResult AddMovie(Movie Mov1)
        {
            Db.Movies.Add(Mov1);
            Db.SaveChanges();
            return Ok(new { message = "Added Successfully" });
        }
        
        [HttpGet("{id}")] // GET api/Movie/5
        public IActionResult GetMovieById(int id)
        {
            var movie = Db.Movies.FirstOrDefault(m => m.id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            return Ok(movie);
        }
        [HttpDelete("{id}")] // DELETE api/Movie/5
        public IActionResult DeleteMovie(int id)
        {
            var movie = Db.Movies.FirstOrDefault(m => m.id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            Db.Movies.Remove(movie);
            Db.SaveChanges();
            return Ok(new { message = "Deleted Successfully" });
        }
        [HttpPut("{id}")] // PUT api/Movie/5
        public IActionResult UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = Db.Movies.FirstOrDefault(m => m.id == id);
            if (movie == null)
            {
                return NotFound(new { message = "Movie not found" });
            }

            // Update fields
            movie.Title = updatedMovie.Title;
            movie.Type = updatedMovie.Type;
            movie.Image = updatedMovie.Image;
            movie.Rating = updatedMovie.Rating;

            Db.SaveChanges();
            return Ok(new { message = "Updated Successfully" });
        }


    }
}
