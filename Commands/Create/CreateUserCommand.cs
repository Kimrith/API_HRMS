using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using BCrypt.Net;

namespace HRMS.API.Commands.Create
{
    public class CreateUserCommand
    {
        private readonly AppDbContext _context;
        public CreateUserCommand(AppDbContext context) => _context = context;

public async Task<int> ExecuteAsync(RegisterUserDto dto)
{
    string filePath = string.Empty;

    if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
    {
        // Define directory to store images
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

        // Generate unique filename
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ProfileImage.FileName);
        filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.ProfileImage.CopyToAsync(stream);
        }
    }

    var user = new User
    {
        Username = dto.Username,
        Email = dto.Email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
        Role = dto.Role,
        ProfileImg = filePath // Saving the local path
    };

    _context.User.Add(user);
    await _context.SaveChangesAsync();
    return user.Id;
}
    }
}