using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesBackend.Entities
{
    public class Movie
    {
      
        public int Id { get; set; }

        public string PosterImage { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public string Overview { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Rating { get; set; }

        public DateOnly ReleaseDate { get; set; }

        // Navigation properties
        public ICollection<Cast> Cast { get; set; }
        public ICollection<MovSources> MovSources { get; set; }
        public ICollection<ProductionCompaines> ProductionCompaines { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
    }
}
