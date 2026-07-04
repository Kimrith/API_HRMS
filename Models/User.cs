using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public enum UserRole { Employee, HR, Admin }
    public enum UserStatus { Active, Inactive }

    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string ProfileImg { get; set; } = string.Empty; 
        
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        // Use Enums instead of raw strings
        public UserRole Role { get; set; } = UserRole.Employee;
        public UserStatus Status { get; set; } = UserStatus.Inactive;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public EmployeeProfile? EmployeeProfile { get; set; }
    }
}