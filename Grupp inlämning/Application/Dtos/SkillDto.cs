using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class SkillDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 200 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a skill level.")]
        public string SkillLevel { get; set; }
    }
}
