using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        MovContext Db;
        public ReviewsController(MovContext _Db)
        {
            Db = _Db;
        }
        [HttpGet]// GET: api/Reviews
        public IActionResult GetAll()
        {
            var reviews = Db.Reviews
                .Include(u=>u.User)
                .Include(m => m.Movie)
                .OrderBy(r => r.MovieId)
                .ToList();
            List<ReviewDetails> reviewsDetails = new List<ReviewDetails>();
            foreach (var review in reviews)
            {
                reviewsDetails.Add(new ReviewDetails
                {
                    Review = review.Review,
                    Rating = review.Rating,
                    MovieTitle = review.Movie.Title,
                    UserName = review.User.Name
                });
            }
            return Ok(reviewsDetails);
        }

        // POST: api/Reviews
        [HttpPost]
        public IActionResult AddReview([FromBody] Reviews newReview)
        {
            if (newReview == null)
                return BadRequest("Review is null");

            Db.Reviews.Add(newReview);
            Db.SaveChanges();
            return Ok(new { message = "Review added successfully" });
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, [FromBody] Reviews updatedReview)
        {
            var review = Db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
                return NotFound("Review not found");

            // Update fields (avoid updating navigation properties here)
            review.Review = updatedReview.Review;
            review.Rating = updatedReview.Rating;
            review.MovieId = updatedReview.MovieId;
            review.UserId = updatedReview.UserId;

            Db.SaveChanges();
            return Ok(new { message = "Review updated successfully" });
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = Db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
                return NotFound("Review not found");

            Db.Reviews.Remove(review);
            Db.SaveChanges();
            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
