using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;
using HRMS.API.Services;
using HRMS.API.Commands.Create;
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

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromForm] RegisterUserDto dto)
        {
            // CHANGED: Use _context.Users (Plural)
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest("Username is already taken.");

            var userId = await _createCommand.ExecuteAsync(dto);
            // CHANGED: Use _context.Users (Plural)
            var user = await _context.Users.FindAsync(userId);

            return new AuthResponseDto
            {
                UserId = user!.Id,
                Username = user.Username,
                // KEEP as Enum (No .ToString()) to match AuthResponseDto definition
                Role = user.Role, 
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginUserDto loginDto)
        {
            // CHANGED: Use _context.Users (Plural)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            return new AuthResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                // KEEP as Enum (No .ToString())
                Role = user.Role, 
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}