using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesBackend.DTOs
{
    public class AddingMovDTO
    {
        public string PosterImage { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public string Overview { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Rating { get; set; }

        public DateOnly ReleaseDate { get; set; }
    }
}
