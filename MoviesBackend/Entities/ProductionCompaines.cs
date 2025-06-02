namespace MoviesBackend.Entities
{
    public class ProductionCompaines
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgURL { get; set; }
        
        // Foreign key
        public int MovieId { get; set; }

        //// Navigation property
        //public Movie Movie { get; set; }
    }
}
