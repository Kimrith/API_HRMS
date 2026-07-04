using Microsoft.AspNetCore.Mvc;
using HRMS.API.DTOs;
using HRMS.API.Queries;
using HRMS.API.Commands.Update;
using HRMS.API.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.API.Controllers
{
    [Authorize] // Protects all endpoints in this controller
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserQueries _queries;
        private readonly UpdateUserCommand _updateCommand;
        private readonly DeleteUserCommand _deleteCommand;

        public UserController(UserQueries queries, UpdateUserCommand update, DeleteUserCommand delete)
        {
            _queries = queries;
            _updateCommand = update;
            _deleteCommand = delete;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetUsers() => Ok(await _queries.GetAllUsersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var user = await _queries.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegisterUserDto dto) 
        {
            return await _updateCommand.ExecuteAsync(id, dto) ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            return await _deleteCommand.ExecuteAsync(id) ? NoContent() : NotFound();
        }
    }
}