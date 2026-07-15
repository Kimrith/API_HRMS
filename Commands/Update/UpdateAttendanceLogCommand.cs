using HRMS.API.Data;
using HRMS.API.DTOs;
using System.Threading.Tasks;

namespace HRMS.API.Commands.Update
{
    public class UpdateAttendanceLogCommand
    {
        private readonly AppDbContext _context;

        public UpdateAttendanceLogCommand(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates an existing attendance log. 
        /// Used primarily for Admin corrections such as updating CheckOut times 
        /// or adjusting the TrackingStatus.
        /// </summary>
        public async Task<bool> ExecuteAsync(int id, AttendanceAdminUpdateDto dto)
        {
            // Find the existing log in the database
            var log = await _context.AttendanceLogs.FindAsync(id);

            // If the record does not exist, return false
            if (log == null)
            {
                return false;
            }

            // Only update CheckOut if the admin actually provided a new value.
            // If they just want to change the status, this prevents overwriting 
            // an existing CheckOut time with a null value.
            if (dto.CheckOut.HasValue)
            {
                log.CheckOut = dto.CheckOut.Value;
            }

            // Map the updated status
            log.TrackingStatus = dto.TrackingStatus;

            // Save changes to the database
            // SaveChangesAsync returns the number of state entries written
            return await _context.SaveChangesAsync() > 0;
        }
    }
}