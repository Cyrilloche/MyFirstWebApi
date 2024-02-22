namespace MyFirstWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = String.Empty;
        public string Lastname { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = String.Empty;
        public string Mail { get; set; } = String.Empty;
    }
}