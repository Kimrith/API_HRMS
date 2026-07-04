using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.API.Models
{
    public class EmployeeProfile
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateStartedWorking { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public decimal ActiveBonus { get; set; }
        public int UnpaidAbsenceDaysYtd { get; set; }
        
        // Navigation property
        public User User { get; set; } = null!;
    }
}