using System;
using System.ComponentModel.DataAnnotations;
using HRMS.API.Models;

namespace HRMS.API.DTOs
{
    // 1. Used for Creating/Updating (Input)
    public class LeaveRequestCreateUpdateDto : IValidatableObject
    {
        [Required(ErrorMessage = "Employee reference is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave type is required.")]
        [StringLength(50, ErrorMessage = "Leave type cannot exceed 50 characters.")]
        public string LeaveType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? Reason { get; set; }

        public Status Status { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.Date < StartDate.Date)
            {
                yield return new ValidationResult(
                    "The end date cannot be earlier than the start date.",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }

    // 2. Used for Reading/Displaying (Output)
    public class LeaveRequestDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty; // Converted to string for UI
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}