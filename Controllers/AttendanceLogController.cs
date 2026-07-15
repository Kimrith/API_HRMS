using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Commands.Create;
using HRMS.API.Commands.Delete;
using HRMS.API.Commands.Update;
using HRMS.API.DTOs;
using HRMS.API.Queries;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceLogController : ControllerBase
    {
        private readonly AttendanceLogQueries _queries;
        private readonly CreateAttendanceLogCommand _create;
        private readonly UpdateAttendanceLogCommand _update;
        private readonly DeleteAttendanceLogCommand _delete;

        public AttendanceLogController(
            AttendanceLogQueries queries, 
            CreateAttendanceLogCommand create, 
            UpdateAttendanceLogCommand update, 
            DeleteAttendanceLogCommand delete)
        {
            _queries = queries;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> ClockIn([FromBody] AttendanceCreateDto dto) 
        {
            var log = await _create.ExecuteAsync(dto);
            return Ok(log);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttendanceAdminUpdateDto dto)
        {
            var success = await _update.ExecuteAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _delete.ExecuteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAll()
        {
            var logs = await _queries.GetAllLogs()
                .Select(a => new AttendanceReadDto 
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeFullName = $"{a.Employee.FirstName} {a.Employee.LastName}",
                    Date = a.Date,
                    FormattedDate = a.FormattedDate,
                    CheckIn = a.CheckIn,
                    CheckOut = a.CheckOut,
                    TrackingStatus = a.TrackingStatus
                }).ToListAsync();
                
            return Ok(logs);
        }
    }
}