using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRMS.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole { Employee, HR, Admin }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserStatus { Active, Inactive }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender { NotSpecified, Male, Female, Other }

    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string ProfileImg { get; set; } = string.Empty; 
        
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.NotSpecified;
        
        public DateTime? DateOfBirth { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public UserRole Role { get; set; } = UserRole.Employee;
        public UserStatus Status { get; set; } = UserStatus.Inactive;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public EmployeeProfile? EmployeeProfile { get; set; }
    }
}