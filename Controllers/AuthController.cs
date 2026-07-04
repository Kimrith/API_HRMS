using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using HRMS.API.Services;
using HRMS.API.Commands.Create; // Ensure this is imported to use the command logic
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly CreateUserCommand _createCommand;

        public AuthController(AppDbContext context, ITokenService tokenService, CreateUserCommand createCommand)
        {
            _context = context;
            _tokenService = tokenService;
            _createCommand = createCommand;
        }


        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromForm] RegisterUserDto dto)
        {
            if (await _context.User.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username is already taken.");

            var userId = await _createCommand.ExecuteAsync(dto);
            var user = await _context.User.FindAsync(userId);

            return new AuthResponseDto
            {
                UserId = user!.Id, // Note: user cannot be null here if created
                Username = user.Username,
                // FIX: Convert Enum to string
                Role = user.Role.ToString(), 
                Token = _tokenService.CreateToken(user)
            };
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginUserDto loginDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                // FIX: Convert Enum to string
                Role = user.Role.ToString(), 
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}