using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Data;

namespace HRMS.API.Queries
{
    public class AttendanceLogQueries
    {
        private readonly AppDbContext _context;

        public AttendanceLogQueries(AppDbContext context)
        {
            _context = context;
        }

        // Get all logs with associated Employee data
        public IQueryable<AttendanceLog> GetAllLogs()
        {
            return _context.AttendanceLogs
                .Include(a => a.Employee)
                .AsNoTracking();
        }

        // Get logs for a specific employee
        public IQueryable<AttendanceLog> GetLogsByEmployee(int employeeId)
        {
            return GetAllLogs().Where(a => a.EmployeeId == employeeId);
        }

        // Get logs for a specific date
        public IQueryable<AttendanceLog> GetLogsByDate(DateTime date)
        {
            return GetAllLogs().Where(a => a.Date.Date == date.Date);
        }

        // Get a single log by ID
        public async Task<AttendanceLog?> GetLogByIdAsync(int id)
        {
            return await _context.AttendanceLogs
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}