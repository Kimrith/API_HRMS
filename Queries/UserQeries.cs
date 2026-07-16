using HRMS.API.Data;
using HRMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;

        public UserQueries(AppDbContext context) => _context = context;

        // Fetch a user by ID and map to UserResponseDto
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            return await _context.Users // Using pluralized 'Users'
                .Where(u => u.Id == id)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,           // Assign Enum directly
                    Status = u.Status,       // Assign Enum directly
                    Gender = u.Gender,       // Added missing fields
                    Nationality = u.Nationality,
                    DateOfBirth = u.DateOfBirth,
                    CreatedAt = u.CreatedAt
                })
                .FirstOrDefaultAsync();
        }

        // Fetch all users
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,           // Assign Enum directly
                    Status = u.Status,       // Assign Enum directly
                    Gender = u.Gender,       // Added missing fields
                    Nationality = u.Nationality,
                    DateOfBirth = u.DateOfBirth,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();
        }
    }
}