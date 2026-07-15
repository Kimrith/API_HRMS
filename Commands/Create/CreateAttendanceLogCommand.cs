using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore; // Added for Querying

namespace HRMS.API.Commands.Create
{
    public class CreateAttendanceLogCommand
    {
        private readonly AppDbContext _context;

        public CreateAttendanceLogCommand(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<AttendanceLog?> ExecuteAsync(AttendanceCreateDto dto)
        {
            // 1. Check if the employee has already checked in today
            var alreadyCheckedIn = await _context.AttendanceLogs
                .AnyAsync(a => a.EmployeeId == dto.EmployeeId && a.Date == DateTime.Today);

            if (alreadyCheckedIn)
            {
                // Return null or handle as an error if they try to check in twice
                return null; 
            }

            // 2. Create new log
            var log = new AttendanceLog
            {
                EmployeeId = dto.EmployeeId,
                Date = DateTime.Today,
                CheckIn = DateTime.Now,
                // Assign the Enum value directly
                TrackingStatus = AttendanceStatus.Present 
            };

            _context.AttendanceLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }
    }
}