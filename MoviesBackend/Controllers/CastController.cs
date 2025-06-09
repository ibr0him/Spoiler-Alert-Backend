using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        MovContext Db;
        public CastController(MovContext _Db)
        {
            Db = _Db;
        }
        [HttpGet("{Id}")]  // GET: api/cast/5
        public IActionResult GetCastByMovieId(int Id)
        {
            var castList = Db.Cast
                .Include(c => c.Movie) // Include Movie navigation property if needed
                .Where(c => c.MovieId == Id)
                .Select(c => new
                {
                    c.Name,
                    c.ImgURL
                })
                .ToList();

            if (castList == null)
                return NotFound();

            return Ok(castList);
        }
        [HttpPost("{id}")] // api/cast
        public IActionResult AddCast([FromBody] List<AddingCastDTO> newCastList, int id)
        {
            
            if (!Db.Movies.Any(m => m.Id == id))
                return NotFound(new { message = "Movie not found" });

            // Convert the DTOs to Cast entities
            var newCast = newCastList.Select(c => new Cast
            {
                Name = c.Name,
                ImgURL = c.ImgURL,
                MovieId = id // Assign the movie ID from the route
            }).ToList();

            // Add the new cast to the database
            Db.Cast.AddRange(newCast);
            Db.SaveChanges();

            return Ok(new
            {
                message = "Cast members added successfully",
                count = newCast.Count,
                movieId = id
            });
        }

        [HttpDelete("{id}")] // api/cast/5
        public IActionResult DeleteCast(int id)
        {
            var cast = Db.Cast.FirstOrDefault(c => c.Id == id);
            if (cast == null)
                return NotFound(new { message = "Cast not found" });
            Db.Cast.Remove(cast);
            Db.SaveChanges();
            return Ok(new { message = "Cast deleted successfully" });

        }
        // PUT: api/Cast/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCast(int id, [FromBody] Cast updated)
        {
            var cast = Db.Cast.FirstOrDefault(c => c.Id == id);
            if (cast == null)
                return NotFound(new { message = "Cast not found" });

            cast.Name = updated.Name;
            cast.ImgURL = updated.ImgURL;

            Db.SaveChanges();
            return Ok(new { message = "Cast updated" });
        }
    }
}
