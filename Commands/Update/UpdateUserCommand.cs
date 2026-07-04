using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models; // Ensure this is included
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
            
            // Fix: Parse the string from the DTO into your Enum
            if (Enum.TryParse<UserRole>(dto.Role, true, out var roleEnum))
            {
                user.Role = roleEnum;
            }
            else
            {
                // Optional: Throw an exception or handle invalid role input
                throw new ArgumentException($"Invalid role provided: {dto.Role}");
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}