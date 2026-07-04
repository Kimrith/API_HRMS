using HRMS.API.Data;
using HRMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Update
{
    public class UpdateUserCommand
    {
        private readonly AppDbContext _context;
        public UpdateUserCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id, RegisterUserDto dto)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return false;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Role = dto.Role;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}