using Microsoft.AspNetCore.Mvc;
using MoviesBackend.Context;
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
        [HttpPost]
        public IActionResult AddCompany(ProductionCompaines newCompany)
        {
            if (newCompany == null)
                return BadRequest(new { message = "Invalid company data." });

            Db.ProductionCompaines.Add(newCompany);
            Db.SaveChanges();
            return Ok(new { message = "Company added successfully." });
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
