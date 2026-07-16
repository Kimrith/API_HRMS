using HRMS.API.Models;

namespace HRMS.API.DTOs
{
    public class UserWithProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        // Use the Enums directly
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        
        // Include the other personal info fields
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string? ProfileImg { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Nested DTO for the profile
        public EmployeeProfileResponseDto? EmployeeProfile { get; set; }
    }
}