using MediatR;
using HRMS.API.DTOs;
using HRMS.API.Data;
using HRMS.API.Models;

namespace HRMS.API.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public LeaveRequestCreateUpdateDto Dto { get; set; }

        public UpdateLeaveRequestCommand(int id, LeaveRequestCreateUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateLeaveRequestCommandHandler(AppDbContext context) => _context = context;

        public async Task<bool> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LeaveRequests.FindAsync(request.Id);
            
            if (entity == null) return false;

            // Apply updates
            entity.LeaveType = request.Dto.LeaveType;
            entity.StartDate = request.Dto.StartDate;
            entity.EndDate = request.Dto.EndDate;
            entity.Reason = request.Dto.Reason;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}