using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HRMS.API.Commands.Create;
using HRMS.API.Commands.Update;
using HRMS.API.Commands.Delete;
using HRMS.API.DTOs;
using HRMS.API.Queries;

namespace HRMS.API.Controllers
{
    // Authorization is currently disabled for testing
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeQueries _queries;
        private readonly UpdateEmployeeCommand _updateCommand;
        private readonly DeleteEmployeeCommand _deleteCommand;
        private readonly CreateEmployeeCommand _createCommand;

        public EmployeesController(
            EmployeeQueries queries, 
            UpdateEmployeeCommand updateCommand, 
            DeleteEmployeeCommand deleteCommand,
            CreateEmployeeCommand createCommand)
        {
            _queries = queries;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
            _createCommand = createCommand;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeProfileResponseDto>>> GetAll()
        {
            return Ok(await _queries.GetAllEmployeesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeProfileResponseDto>> GetById(int id)
        {
            var employee = await _queries.GetEmployeeByIdAsync(id);
            return employee != null ? Ok(employee) : NotFound($"Employee with ID {id} not found.");
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            // Pass the entire DTO to match the updated method signature
            var newId = await _createCommand.ExecuteAsync(dto);
            
            return Ok(new { message = "Employee created successfully.", id = newId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeProfileDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _updateCommand.ExecuteAsync(id, dto);
            return success ? NoContent() : NotFound($"Employee with ID {id} not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deleteCommand.ExecuteAsync(id);
            return success ? NoContent() : NotFound($"Employee with ID {id} not found.");
        }
    }
}