using HRMS.API.Data;
using HRMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;

        public UserQueries(AppDbContext context)
        {
            _context = context;
        }

        // Fetch a user by ID and map to UserResponseDto
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            return await _context.User
                .Where(u => u.Id == id)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    Status = u.Status,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        // Fetch all users
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.User
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    Status = u.Status,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }
    }
}