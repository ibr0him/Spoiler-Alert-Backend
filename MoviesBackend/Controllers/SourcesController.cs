using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Context;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        MovContext Db;
        public SourcesController(MovContext _Db)
        {
            Db = _Db;
        }
        [HttpGet("{Id}")]  // GET: api/sources/5
        public IActionResult GetSourcesByMovieId(int Id)
        {
            var sources = Db.MovSources
                            .Where(s => s.MovieId == Id)
                            .Select(s => new
                            {
                                s.Img1,
                                s.Img2,
                                s.Video
                            })
                            .ToList();

            if (!sources.Any())
                return NotFound(new { message = "No sources found for this movie." });

            return Ok(sources);
        }
        // POST: api/sources
        [HttpPost]
        public IActionResult AddSource([FromBody] MovSources source)
        {
            Db.MovSources.Add(source);
            Db.SaveChanges();
            return Ok(new { message = "Source added successfully" });
        }

        // PUT: api/sources/5
        [HttpPut("{id}")]
        public IActionResult UpdateSource(int id, [FromBody] MovSources updatedSource)
        {
            var source = Db.MovSources.FirstOrDefault(s => s.Id == id);
            if (source == null)
                return NotFound(new { message = "Source not found" });

            source.Img1 = updatedSource.Img1;
            source.Img2 = updatedSource.Img2;
            source.Video = updatedSource.Video;
            source.MovieId = updatedSource.MovieId;

            Db.SaveChanges();
            return Ok(new { message = "Source updated successfully" });
        }

        // DELETE: api/sources/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSource(int id)
        {
            var source = Db.MovSources.FirstOrDefault(s => s.Id == id);
            if (source == null)
                return NotFound(new { message = "Source not found" });

            Db.MovSources.Remove(source);
            Db.SaveChanges();
            return Ok(new { message = "Source deleted successfully" });
        }

    }
}
