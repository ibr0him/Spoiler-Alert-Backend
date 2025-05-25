using Microsoft.EntityFrameworkCore;
using MoviesBackend.Entities;

namespace MoviesBackend.Context
{
    public class MovContext : DbContext
    {
        public MovContext(DbContextOptions<MovContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
    }
}
