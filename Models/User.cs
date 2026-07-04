using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        // Corrected the typo in your original snippet
        public string ProfileImg { get; set; } = string.Empty; 
        
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string Role { get; set; } = "Employee";
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to link User to their Profile
        public EmployeeProfile? EmployeeProfile { get; set; }
    }
}