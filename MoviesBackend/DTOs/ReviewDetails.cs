namespace MoviesBackend.DTOs
{
    public class ReviewDetails
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string MovieTitle { get; set; }
        public string UserName { get; set; }
    }
}
