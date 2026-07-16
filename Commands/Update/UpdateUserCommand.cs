using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Update
{
    public class UpdateUserCommand
    {
        private readonly AppDbContext _context;

        public UpdateUserCommand(AppDbContext context) => _context = context;

        // I updated this signature from RegisterUserDto to UpdateUserDto
        public async Task<bool> ExecuteAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            // Update basic fields
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.Gender = dto.Gender;
            user.DateOfBirth = dto.DateOfBirth;
            user.Nationality = dto.Nationality;
            
            try 
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}