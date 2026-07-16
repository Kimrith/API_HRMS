using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting; // Required for IWebHostEnvironment

namespace HRMS.API.Commands.Delete
{
    public class DeleteUserCommand
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteUserCommand(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            // 1. Use the pluralized name 'Users' (if your DbContext follows conventions)
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            // 2. Cleanup: Delete the profile image from the server
            if (!string.IsNullOrEmpty(user.ProfileImg))
            {
                var fullPath = Path.Combine(_env.WebRootPath, user.ProfileImg);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            // 3. Remove and Save
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}