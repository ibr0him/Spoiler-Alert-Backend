using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesBackend.Entities
{
    public class Movie
    {
      
        public int id { get; set; }

        public string Image { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        
        [Column(TypeName = "varchar(3)")]
        public string Rating { get; set; }
    }
}
