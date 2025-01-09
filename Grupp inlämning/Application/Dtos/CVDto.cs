using System.ComponentModel.DataAnnotations;


namespace Application.Dtos
{
    public class CVDto
    {

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters.")]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public ContactDetailsDto? ContactDetails { get; set; }
        public UserDto? User { get; set; }
        public AboutDto? About { get; set; }
        public ICollection<WorkExperienceDto> WorkExperiences { get; set; } = new List<WorkExperienceDto>();
        public ICollection<EducationDto> Educations { get; set; } = new List<EducationDto>();
        public ICollection<SkillDto> Skills { get; set; } = new List<SkillDto>();
    }
}
