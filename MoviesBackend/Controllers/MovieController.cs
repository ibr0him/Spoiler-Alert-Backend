using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
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

        [HttpGet] // GET: api/movie
        public IActionResult GetAll()
        {
            var movies = Db.Movies
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.PosterImage,
                    m.Genres,
                    m.Overview,
                    m.Rating,
                    m.ReleaseDate
                })
                .ToList();

            return Ok(movies);
        }
        [HttpGet("{id}")] // GET: api/movie/5
        public IActionResult GetById(int id)
        {
            var movie = Db.Movies
                .Include(m => m.Cast)
                .Include(m => m.MovSources)
                .Include(m => m.ProductionCompaines)
                .Include(m => m.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            MovieDetails movieDetails = new MovieDetails
            {
                Cast = movie.Cast?.Select(c => new
                {
                    c.Name,
                    c.ImgURL
                }).ToList<object>(),

                ProductionCompanies = movie.ProductionCompaines?.Select(pc => new
                {
                    pc.Name,
                    pc.ImgURL
                }).ToList<object>(),

                Sources = movie.MovSources != null ? new
                {
                    Img1 = movie.MovSources.Img1,
                    Img2 = movie.MovSources.Img2,
                    Video = movie.MovSources.Video
                } : null,

                Reviews = movie.Reviews?.Select(c => new ReviewDetails
                {
                    Id = c.Id,
                    Review = c.Review,
                    Rating = c.Rating,
                    MovieTitle = c.Movie?.Title,
                    UserName = c.User?.Name
                }).ToList()
            };

            return Ok(movieDetails);
        }


        [HttpPost] //api/Movie --> Post
        public IActionResult AddMovie([FromBody] AddingMovDTO Mov1)
        {
            if (Mov1 == null)
                return BadRequest(new { message = "Invalid movie data" });
            // Assuming AddingMovDTO has properties that match Movie
            var movie = new Movie
            {
                Title = Mov1.Title,
                Genres = Mov1.Genres,
                PosterImage = Mov1.PosterImage,
                Overview = Mov1.Overview,
                Rating = Mov1.Rating,
                ReleaseDate = Mov1.ReleaseDate
            };
            Db.Movies.Add(movie);
            Db.SaveChanges();
            return Ok(new { message = "Added Successfully" , id=movie.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = Db.Movies
                          .Include(m => m.Cast)
                          .Include(m => m.MovSources)
                          .Include(m => m.ProductionCompaines)
                          .Include(m => m.Reviews)
                          .FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound(new { message = "Movie not found" });

            // Optional: Remove related data manually if not configured for cascade delete
            Db.Movies.Remove(movie);
            Db.SaveChanges();

            return Ok(new { message = "Deleted Successfully" });
        }
                          
        [HttpPut("{id}")] // PUT: api/Movie/{id}
        public IActionResult UpdateMovie(int id, [FromBody] Movie updatedMovie)
        {
            var movie = Db.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound(new { message = "Movie not found" });

            // Update only scalar properties here
            movie.Title = updatedMovie.Title;
            movie.Genres = updatedMovie.Genres;
            movie.PosterImage = updatedMovie.PosterImage;
            movie.Rating = updatedMovie.Rating;
            movie.Overview = updatedMovie.Overview;
            movie.ReleaseDate = updatedMovie.ReleaseDate;

            Db.SaveChanges();

            return Ok(new { message = "Updated Successfully" });
        }


    }
}
