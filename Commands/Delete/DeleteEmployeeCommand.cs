using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Delete
{
    public class DeleteEmployeeCommand
    {
        private readonly AppDbContext _context;

        public DeleteEmployeeCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int employeeId)
        {
            var profile = await _context.EmployeeProfile
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (profile == null) return false;

            _context.EmployeeProfile.Remove(profile);
            
            // If you want to delete the associated User as well:
            // var user = await _context.User.FindAsync(profile.UserId);
            // if (user != null) _context.User.Remove(user);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}