using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class Skill
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public enum Level
        {
            Beginner,
            Intermediate,
            Advanced,
            Expert
        }
        public Level SkillLevel { get; set; }

        public string Description { get; set; }
        public Guid CvId { get; set; }
        public CV Cv { get; set; }

        public Skill(Guid id, string name, Level skillLevel, string description)
        {
            Id = id;
            Name = name;
            SkillLevel = skillLevel;
            Description = description;
        }
    }
}
