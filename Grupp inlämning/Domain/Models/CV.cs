namespace Domain.Models
{
    public class CV
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public ContactDetail ContactDetails { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public About About { get; set; }

        public CV() { }

        public CV(string title, Guid userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
