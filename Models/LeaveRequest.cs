using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public enum Status {
        Pending,
        Approved,
        Rejected
    }

    public class LeaveRequest
    {
        public int Id { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LeaveType { get; set; } = string.Empty; // e.g., Sick, Vacation, Personal
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        public Status Status { get; set; }
        
        public string? Reason { get; set; } // Optional: Employee's note
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}