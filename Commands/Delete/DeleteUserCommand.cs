using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Delete
{
    public class DeleteUserCommand
    {
        private readonly AppDbContext _context;
        public DeleteUserCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id)
        {
            // Find the user first
            var user = await _context.User.FindAsync(id);
            if (user == null) return false;

            // Remove the user from the database
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}