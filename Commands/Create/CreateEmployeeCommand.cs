using HRMS.API.Data;
using HRMS.API.Models;

namespace HRMS.API.Commands.Create
{
    public class CreateEmployeeCommand
    {
        private readonly AppDbContext _context;

        public CreateEmployeeCommand(AppDbContext context) => _context = context;

        public async Task<int> ExecuteAsync(int userId, string firstName, string lastName)
        {
            // Create a default profile linked to the newly created User ID
            var profile = new EmployeeProfile
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Level = JobLevel.Junior, // Default value
                DateOfBirth = DateTime.UtcNow.AddYears(-20), // Placeholder
                DateStartedWorking = DateTime.UtcNow,
                PhoneNumber = string.Empty,
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