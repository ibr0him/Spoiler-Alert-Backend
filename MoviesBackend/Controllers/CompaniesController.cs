using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Context;
using MoviesBackend.DTOs;
using MoviesBackend.Entities;

namespace MoviesBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        MovContext Db;
        public CompaniesController(MovContext _Db)
        {
            Db = _Db;
        }
        [HttpGet("{Id}")]  // GET: api/companies/5
        public IActionResult GetCompaniesByMovieId(int Id)
        {
            var companiesList = Db.ProductionCompaines
                .Where(c => c.MovieId == Id)
                .Select(c => new
                {
                    c.Name,
                    c.ImgURL
                })
                .ToList();

            if (!companiesList.Any())
                return NotFound(new { message = "No companies found for this movie." });

            return Ok(companiesList);
        }

        // POST: api/companies
        [HttpPost("{id}")]
        public IActionResult AddCompany(int id,List<AddingCompanyDTO> newCompany)
        {
            if (!Db.Movies.Any(m => m.Id == id))
                return NotFound(new { message = "Movie not found." });

            // Convert the DTOs to ProductionCompaines entities
            var companies = newCompany.Select(c => new ProductionCompaines
            {
                Name = c.Name,
                ImgURL = c.ImgURL,
                MovieId = id // Assign the movie ID from the route
            }).ToList();

            // Add the new companies to the database
            Db.ProductionCompaines.AddRange(companies);
            Db.SaveChanges();
            return Ok(new
            {
                message = "Companies added successfully",
                count = companies.Count,
                movieId = id
            });

        }

        // PUT: api/companies/5
        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, ProductionCompaines updatedCompany)
        {
            var company = Db.ProductionCompaines.FirstOrDefault(c => c.Id == id);
            if (company == null)
                return NotFound(new { message = "Company not found." });

            // Update fields
            company.Name = updatedCompany.Name;
            company.ImgURL = updatedCompany.ImgURL;
            company.MovieId = updatedCompany.MovieId;

            Db.SaveChanges();
            return Ok(new { message = "Company updated successfully." });
        }

        // DELETE: api/companies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = Db.ProductionCompaines.FirstOrDefault(c => c.Id == id);
            if (company == null)
                return NotFound(new { message = "Company not found." });

            Db.ProductionCompaines.Remove(company);
            Db.SaveChanges();
            return Ok(new { message = "Company deleted successfully." });
        }
    }
}
