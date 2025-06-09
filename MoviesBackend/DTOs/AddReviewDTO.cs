namespace MoviesBackend.DTOs
{
    public class AddReviewDTO
    {
        public string Review { get; set; }
        public int Rating { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
