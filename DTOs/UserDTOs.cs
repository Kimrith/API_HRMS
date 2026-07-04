using System.ComponentModel.DataAnnotations;

namespace HRMS.API.DTOs
{
        public class RegisterUserDto
        {
            public IFormFile? ProfileImage { get; set; }
            [Required, StringLength(50)] public string Username { get; set; } = string.Empty;
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
            public string Role { get; set; } = "Employee"; // Validated in Controller
        }

    // Used when a user logs in (Input)
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    // Used to send User data to the frontend (Output)
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}