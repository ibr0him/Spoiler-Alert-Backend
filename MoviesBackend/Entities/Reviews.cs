namespace MoviesBackend.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }

        //Foreign keys
        public int MovieId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Movie Movie { get; set; }
        public Users User { get; set; }

    }
}
