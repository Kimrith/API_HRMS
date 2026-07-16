using Microsoft.AspNetCore.Mvc;
using HRMS.API.DTOs;
using HRMS.API.Queries;
using HRMS.API.Commands.Update;
using HRMS.API.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.API.Controllers
{
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
        public async Task<ActionResult<List<UserResponseDto>>> GetUsers() 
            => Ok(await _queries.GetAllUsersAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> GetById([FromRoute] int id) 
        {
            var user = await _queries.GetUserByIdAsync(id);
            return user == null ? NotFound(new { message = "User not found" }) : Ok(user);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto dto) // Change Register to Update
        {
            var success = await _updateCommand.ExecuteAsync(id, dto);
            return success ? NoContent() : NotFound(new { message = "User not found" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            return await _deleteCommand.ExecuteAsync(id) ? NoContent() : NotFound();
        }
    }
}