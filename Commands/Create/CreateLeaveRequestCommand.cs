using MediatR;
using HRMS.API.DTOs;
using HRMS.API.Models;
using HRMS.API.Data;

namespace HRMS.API.Commands
{
    // The Command object (holds the data)
    public class CreateLeaveRequestCommand : IRequest<int>
    {
        public CreateLeaveRequestCommand(LeaveRequestCreateUpdateDto dto)
        {
            Dto = dto;
        }

        public LeaveRequestCreateUpdateDto Dto { get; }
    }

    // The Command Handler (contains the business logic)
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateLeaveRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = new LeaveRequest
            {
                EmployeeId = request.Dto.EmployeeId,
                LeaveType = request.Dto.LeaveType,
                StartDate = request.Dto.StartDate,
                EndDate = request.Dto.EndDate,
                Reason = request.Dto.Reason,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // Return the ID of the newly created request
            return entity.Id;
        }
    }
}