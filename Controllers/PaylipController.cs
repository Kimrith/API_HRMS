using Microsoft.AspNetCore.Mvc;
using HRMS.API.Commands.Create;
using HRMS.API.Commands.Update;
using HRMS.API.Commands.Delete;
using HRMS.API.DTOs;
using HRMS.API.Queries;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayslipsController : ControllerBase
    {
        private readonly PayslipQueries _queries;
        private readonly CreatePayslipCommand _createCommand;
        private readonly UpdatePayslipCommand _updateCommand;
        private readonly DeletePayslipCommand _deleteCommand;

        public PayslipsController(
            PayslipQueries queries,
            CreatePayslipCommand createCommand,
            UpdatePayslipCommand updateCommand,
            DeletePayslipCommand deleteCommand)
        {
            _queries = queries;
            _createCommand = createCommand;
            _updateCommand = updateCommand;
            _deleteCommand = deleteCommand;
        }

        // GET: api/payslips
        [HttpGet]
        public async Task<ActionResult<List<PayslipReadDto>>> GetAll()
        {
            return Ok(await _queries.GetAllAsync());
        }

        // POST: api/payslips
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PayslipCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newId = await _createCommand.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = newId }, new { message = "Payslip created successfully.", id = newId });
        }

        // PUT: api/payslips/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] PayslipUpdateStatusDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _updateCommand.ExecuteAsync(id, dto);
            return success ? NoContent() : NotFound($"Payslip with ID {id} not found.");
        }

        // DELETE: api/payslips/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _deleteCommand.ExecuteAsync(id);
            return success ? NoContent() : NotFound($"Payslip with ID {id} not found.");
        }
    }
}