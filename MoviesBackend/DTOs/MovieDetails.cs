using MoviesBackend.Entities;

namespace MoviesBackend.DTOs
{
    public class MovieDetails
    {
        public List<object> Cast { get; set; } = new List<object>();
        public List<object> ProductionCompanies { get; set; } = new List<object>();
        public object Sources { get; set; } = new object();
        public List<ReviewDetails> Reviews { get; set; } = new List<ReviewDetails>();
    }
}
