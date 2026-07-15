using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.API.Models
{
    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late,
        HalfDay,
        OnLeave
    }
    public class AttendanceLog
    {
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public EmployeeProfile Employee { get; set; } = null!;
        
        // This stores the actual date (e.g., 2026-07-07)
        public DateTime Date { get; set; } = DateTime.Today;

        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        
        public AttendanceStatus TrackingStatus { get; set; }

        // This property is NOT stored in the database, 
        // but provides the format you requested for the UI
        [NotMapped]
        public string FormattedDate => Date.ToString("ddd MMM yyyy").ToUpper();
    }
}