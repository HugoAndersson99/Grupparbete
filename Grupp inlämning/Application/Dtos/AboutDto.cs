using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class AboutDto
    {
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string Description { get; set; }
    }
}
