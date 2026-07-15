using HRMS.API.Data; // Ensure this matches your namespace for AppDbContext
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Delete
{
    public class DeleteAttendanceLogCommand
    {
        private readonly AppDbContext _context;

        public DeleteAttendanceLogCommand(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes an attendance record by ID. 
        /// Returns true if successful, false if the record was not found or the database update failed.
        /// </summary>
        public async Task<bool> ExecuteAsync(int id)
        {
            // Fetch the entity
            var log = await _context.AttendanceLogs.FindAsync(id);
            
            // If not found, return false
            if (log == null)
            {
                return false;
            }

            // Remove the entity and save changes
            _context.AttendanceLogs.Remove(log);
            
            // SaveChangesAsync returns the number of state entries written to the DB
            var result = await _context.SaveChangesAsync();
            
            return result > 0;
        }
    }
}