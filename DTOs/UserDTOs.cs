using System.ComponentModel.DataAnnotations;
using HRMS.API.Models; // Assuming your Enums are here

namespace HRMS.API.DTOs
{
    public class RegisterUserDto
    {
        public IFormFile? ProfileImage { get; set; }
        
        [Required, StringLength(50)] 
        public string Username { get; set; } = string.Empty;
        
        [Required, EmailAddress] 
        public string Email { get; set; } = string.Empty;
        
        [Required, MinLength(8)] 
        public string Password { get; set; } = string.Empty;

        // Use the actual Enum here for type safety
        public UserRole Role { get; set; } = UserRole.Employee;
        
        // Add fields that you updated in your model
        public Gender Gender { get; set; } = Gender.NotSpecified;
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
    }

    // --- Add this class inside your DTOs.cs file ---
    public class UpdateUserDto
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public UserRole Role { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;
    }

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
        
        // Return the Enum; if you used [JsonConverter] in the model,
        // this will automatically show as a string in the API response.
        public UserRole Role { get; set; }
    }

    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        // Strongly typed for consistency
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        
        public string? ProfileImg { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}