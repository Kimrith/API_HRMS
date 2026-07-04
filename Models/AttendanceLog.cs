namespace HRMS.API.Models
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; } // Nullable if still checked in
        public string TrackingStatus { get; set; } = "Present";
    }
}