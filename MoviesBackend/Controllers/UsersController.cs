using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
using MoviesBackend.Entities;
using System.ComponentModel.DataAnnotations;

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
        
        [HttpPost("login")] // Post: api/Users/Login
        public IActionResult GetUserByEmailPassword([FromBody] LoginRequest request)
        {
            var user = Db.Users
                .Include(u => u.Reviews)
                .ThenInclude(r => r.Movie)
                .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return NotFound("User not found");

            var UserDTO = new
            {
                user.Id,
                user.Email,
                user.Password,
                user.Name,
                user.isAdmin,
            };

            var Reviews = Db.Reviews
                .Include(r => r.Movie)
                .ThenInclude(m => m.MovSources)
                .Where(r => r.UserId == user.Id)
                .Select(r => new
                {
                    Id = r.Id,
                    Review = r.Review,
                    Rating = r.Rating,
                    MovieTitle = r.Movie.Title,
                    LanscapeImage = r.Movie.MovSources.Img1
                })
                .ToList();

            return Ok(new { UserDTO.Id, UserDTO.Email, UserDTO.Password, UserDTO.Name, UserDTO.isAdmin, Reviews });
        }

        [HttpPost("register")] // POST: api/Users/Register
        public IActionResult AddUser([FromBody] Users newUser)
        {
            if (newUser == null)
                return BadRequest("User is null");
            
            if( Db.Users.FirstOrDefault(u=>u.Email==newUser.Email) != null )
                return BadRequest("User with this email already exists");

            Db.Users.Add(newUser);
            Db.SaveChanges();
            return Ok(new { message = "User added successfully" });
        }

        
        [HttpPut("{id}")]  // PUT: api/Users/5
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

        [HttpDelete("{id}")] // DELETE: api/Users/5
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
