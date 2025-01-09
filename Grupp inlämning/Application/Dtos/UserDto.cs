using System.ComponentModel.DataAnnotations;


namespace Application.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Email must be between 2 and 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Password must be at least 3 characters long.")]
        public string PasswordHash { get; set; }
    }
}
