using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands.Update
{
    public class UpdateEmployeeCommand
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id, UpdateEmployeeProfileDto dto)
        {
            var profile = await _context.EmployeeProfile.FindAsync(id);
            if (profile == null) return false;

            // Update fields only if they are provided in the DTO
            if (!string.IsNullOrWhiteSpace(dto.FirstName)) {
                profile.FirstName = dto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(dto.LastName)){
                profile.LastName = dto.LastName;
            } 

            if (dto.PhoneNumber.HasValue) {
                profile.PhoneNumber = dto.PhoneNumber.Value;
            } 

            // Inside UpdateEmployeeCommand.cs
            if (!string.IsNullOrWhiteSpace(dto.Department)) {
                profile.Department = dto.Department; 
            } 
            
            // Handle Enum parsing for JobLevel
            if (!string.IsNullOrWhiteSpace(dto.Level))
            {
                if (Enum.TryParse<JobLevel>(dto.Level, true, out var levelEnum))
                {
                    profile.Level = levelEnum;
                }
            }

            if (dto.BasicSalary.HasValue)
            {
                profile.BasicSalary = dto.BasicSalary.Value;
            }

            _context.EmployeeProfile.Update(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}