using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        MovContext Db;
        public UsersController(MovContext _Db)
        {
            Db = _Db;
        }
        // GET: api/Users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = Db.Users
                .Include(u => u.Reviews)
                .ThenInclude(r => r.Movie)
                .ToList();

            var UserDTO = users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.isAdmin,
            }).ToList();
            return Ok(UserDTO);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserReviews(int id) { 
            
            var Reviews = Db.Reviews
            .Include(r => r.Movie)
            .Include(r => r.User)
            .Where(r => r.UserId == id)
            .Select(r => new ReviewDetails
            {
                Review = r.Review,
                Rating = r.Rating,
                MovieTitle = r.Movie.Title,
                UserName = r.User.Name
            })
            .ToList();

            //return Ok(Reviews);
            return Ok(Reviews);

        }


        // POST: api/Users
        [HttpPost]
        public IActionResult AddUser([FromBody] Users newUser)
        {
            if (newUser == null)
                return BadRequest("User is null");

            Db.Users.Add(newUser);
            Db.SaveChanges();
            return Ok(new { message = "User added successfully" });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users updatedUser)
        {
            var user = Db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            Db.SaveChanges();
            return Ok(new { message = "User updated successfully" });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = Db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found");

            Db.Users.Remove(user);
            Db.SaveChanges();
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
