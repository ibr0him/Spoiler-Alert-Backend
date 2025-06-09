namespace MoviesBackend.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public bool isAdmin { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
