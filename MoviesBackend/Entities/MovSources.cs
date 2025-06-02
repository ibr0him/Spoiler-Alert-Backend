namespace MoviesBackend.Entities
{
    public class MovSources
    {
        public int Id { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Video { get; set; }

        // Foreign key
        public int MovieId { get; set; }

        //// Navigation property
        //public Movie Movie { get; set; }
    }
}
