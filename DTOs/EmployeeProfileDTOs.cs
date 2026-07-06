using System.ComponentModel.DataAnnotations;

namespace HRMS.API.DTOs
{
    public class EmployeeProfileResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Level { get; set; } = string.Empty;
        public string Department {get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateStartedWorking { get; set; }
        public long PhoneNumber { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal ActiveBonus { get; set; }
        public int UnpaidAbsenceDaysYtd { get; set; }
    }

    public class CreateEmployeeDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
    }

    public class UpdateEmployeeProfileDto
    {
        [StringLength(50)] public string? FirstName { get; set; }
        [StringLength(50)] public string? LastName { get; set; }
        public long? PhoneNumber { get; set; }
        public string? Level { get; set; }
        public string? Department {get; set;}
        public decimal? BasicSalary { get; set; }
    }
}