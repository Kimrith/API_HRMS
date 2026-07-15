using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.API.DTOs;
using HRMS.API.Commands;
using HRMS.API.Queries;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllLeaveRequestsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLeaveRequestByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequestCreateUpdateDto dto)
        {
            var id = await _mediator.Send(new CreateLeaveRequestCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LeaveRequestCreateUpdateDto dto)
        {
            var success = await _mediator.Send(new UpdateLeaveRequestCommand(id, dto));
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteLeaveRequestCommand(id));
            return success ? NoContent() : NotFound();
        }
    }
}