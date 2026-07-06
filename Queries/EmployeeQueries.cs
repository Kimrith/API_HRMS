using HRMS.API.Data;
using HRMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Queries
{
    public class EmployeeQueries
    {
        private readonly AppDbContext _context;

        public EmployeeQueries(AppDbContext context)
        {
            _context = context;
        }

        // Fetch a single employee profile by their ID
        public async Task<EmployeeProfileResponseDto?> GetEmployeeByIdAsync(int id)
        {
            return await _context.EmployeeProfile
                .Where(e => e.Id == id)
                .Select(e => new EmployeeProfileResponseDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    // Convert Enum to string for the DTO
                    Level = e.Level.ToString(),
                    Department = e.Department,
                    DateOfBirth = e.DateOfBirth,
                    DateStartedWorking = e.DateStartedWorking,
                    PhoneNumber = e.PhoneNumber,
                    BasicSalary = e.BasicSalary,
                    ActiveBonus = e.ActiveBonus,
                    UnpaidAbsenceDaysYtd = e.UnpaidAbsenceDaysYtd
                })
                .FirstOrDefaultAsync();
        }

        // Fetch all employee profiles
        public async Task<List<EmployeeProfileResponseDto>> GetAllEmployeesAsync()
        {
            return await _context.EmployeeProfile
                .Select(e => new EmployeeProfileResponseDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Level = e.Level.ToString(),
                    Department = e.Department,
                    DateOfBirth = e.DateOfBirth,
                    DateStartedWorking = e.DateStartedWorking,
                    PhoneNumber = e.PhoneNumber,
                    BasicSalary = e.BasicSalary,
                    ActiveBonus = e.ActiveBonus,
                    UnpaidAbsenceDaysYtd = e.UnpaidAbsenceDaysYtd
                })
                .ToListAsync();
        }
    }
}