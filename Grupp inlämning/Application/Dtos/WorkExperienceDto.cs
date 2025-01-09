using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class WorkExperienceDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 200 characters.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Job title must be between 2 and 200 characters.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 200 characters.")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "City must be between 2 and 200 characters.")]
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
