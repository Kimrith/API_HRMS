using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.API.Models
{
    public enum JobLevel { Internship ,Junior, Mid, Senior, Lead }

    public class EmployeeProfile
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        public JobLevel Level { get; set; } = JobLevel.Junior;

        [StringLength(100)]
        public string Department { get; set; } = string.Empty;
        
        public DateTime DateOfBirth { get; set; }
        public DateTime DateStartedWorking { get; set; }
        
        [Phone]
        public long PhoneNumber { get; set; }
        
        [Column(TypeName = "decimal(18,2)")] // Essential for SQL money types
        public decimal BasicSalary { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ActiveBonus { get; set; }
        
        public int UnpaidAbsenceDaysYtd { get; set; }
        
        public User User { get; set; } = null!;
    }
}