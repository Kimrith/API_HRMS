using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.API.DTOs;
using HRMS.API.Queries;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/EmployeeProfiles
        // Returns the full list of Users including their related EmployeeProfile data
        [HttpGet]
        public async Task<ActionResult<List<UserWithProfileDto>>> GetAllProfiles()
        {
            // The mediator sends the query to GetAllUsersQueryHandler
            // This returns the full User + Profile projection
            var result = await _mediator.Send(new GetAllUsersQuery());
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return result != null ? Ok(result) : NotFound($"Employee with ID {id} not found.");
        }
    }
}