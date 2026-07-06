using HRMS.API.Data;
using HRMS.API.Models;
using HRMS.API.DTOs;

namespace HRMS.API.Commands.Create
{
    public class CreateEmployeeCommand
    {
        private readonly AppDbContext _context;

        public CreateEmployeeCommand(AppDbContext context) => _context = context;

        // Accepting the DTO here fixes the scope error
        public async Task<int> ExecuteAsync(CreateEmployeeDto dto)
        {
            var profile = new EmployeeProfile
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Department = dto.Department, // Now correctly pulling from the DTO
                Level = JobLevel.Junior,
                DateOfBirth = DateTime.UtcNow.AddYears(-20), 
                DateStartedWorking = DateTime.UtcNow,
                PhoneNumber = dto.PhoneNumber,
                BasicSalary = 0,
                ActiveBonus = 0,
                UnpaidAbsenceDaysYtd = 0
            };

            _context.EmployeeProfile.Add(profile);
            await _context.SaveChangesAsync();
            
            return profile.Id;
        }
    }
}