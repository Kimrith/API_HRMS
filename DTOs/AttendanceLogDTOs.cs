using System;
using HRMS.API.Models; // Ensure this is here to access AttendanceStatus

namespace HRMS.API.DTOs
{
    // Used for retrieving attendance records
    public class AttendanceReadDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public AttendanceStatus TrackingStatus { get; set; }
        public string FormattedDate { get; set; } = string.Empty;
    }

    // Used when an employee initiates a Clock-In via QR
    public class AttendanceCreateDto
    {
        public int EmployeeId { get; set; }
    }

    // Used strictly by Admins to modify existing logs
    public class AttendanceAdminUpdateDto
    {
        // Made nullable so admins aren't forced to submit a default date
        public DateTime? CheckOut { get; set; } 
        
        public AttendanceStatus TrackingStatus { get; set; }
    }
}