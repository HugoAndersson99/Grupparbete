using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class EducationDto
    {
        [Required(ErrorMessage = "School name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "School name must be between 2 and 200 characters.")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Degree is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Degree must be between 2 and 100 characters.")]
        public string Degree { get; set; }

        [StringLength(100, ErrorMessage = "Field of study cannot exceed 100 characters.")]
        public string FieldOfStudy { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
        public string City { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }
    }
}
