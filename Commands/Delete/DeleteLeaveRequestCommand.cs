using MediatR;
using HRMS.API.Data; // Ensure this matches your namespace
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteLeaveRequestCommand(int id) => Id = id;
    }

    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLeaveRequestCommandHandler(AppDbContext context) => _context = context;

        public async Task<bool> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LeaveRequests.FindAsync(request.Id);
            
            if (entity == null) return false;

            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}