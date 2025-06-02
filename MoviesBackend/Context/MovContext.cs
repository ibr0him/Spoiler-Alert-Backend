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
        public DbSet<Cast> Cast { get; set; }
        public DbSet<MovSources> MovSources { get; set; }
        public DbSet<ProductionCompaines> ProductionCompaines { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
