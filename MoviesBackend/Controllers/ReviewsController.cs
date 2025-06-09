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
        [HttpGet]// GET: api/reviews
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
                    Id = review.Id,
                    Review = review.Review,
                    Rating = review.Rating,
                    MovieTitle = review.Movie.Title,
                    UserName = review.User.Name
                });
            }
            return Ok(reviewsDetails);
        }

        // POST: api/reviews
        [HttpPost]
        public IActionResult AddReview([FromBody] AddReviewDTO InputReview)
        {
           if(InputReview == null)
                return BadRequest("Invalid review data");

            var newReview = new Reviews
            {
                Review = InputReview.Review,
                Rating = InputReview.Rating,
                MovieId = InputReview.MovieId,
                UserId =  InputReview.UserId
            };

            Db.Reviews.Add(newReview);
            Db.SaveChanges();
            return Ok(new
            {
                message = "Review added successfully",
                id = newReview.Id
            });
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, [FromBody] UpdateReviewDTO updatedReview)
        {
            var review = Db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
                return NotFound("Review not found");

           review.Review = updatedReview.Review;
           review.Rating = updatedReview.Rating;


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
