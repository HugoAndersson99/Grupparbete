namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public ICollection<CV> CVs { get; set; } = new List<CV>();

        public User() { }

        public User(string email, string passwordHash, string role)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
