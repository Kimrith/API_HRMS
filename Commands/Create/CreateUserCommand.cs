using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using BCrypt.Net;

namespace HRMS.API.Commands.Create
{
    public class CreateUserCommand
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        // Inject IWebHostEnvironment to get the correct path dynamically
        public CreateUserCommand(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<int> ExecuteAsync(RegisterUserDto dto)
        {
            string relativePath = string.Empty;

            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                // Use _env.WebRootPath to ensure it works across different environments (Linux/Windows)
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ProfileImage.FileName)}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ProfileImage.CopyToAsync(stream);
                }
                
                // Save only the relative path to the DB
                relativePath = Path.Combine("uploads", fileName);
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role, // Already an Enum, no parsing needed!
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                ProfileImg = relativePath,
                Status = UserStatus.Active // Defaulting to active upon registration
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user.Id;
        }
    }
}