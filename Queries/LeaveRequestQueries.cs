using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.API.DTOs;
using HRMS.API.Data;
using HRMS.API.Models; // REQUIRED: To access the LeaveRequest entity and Status enum

namespace HRMS.API.Queries
{
    public class GetAllLeaveRequestsQuery : IRequest<List<LeaveRequestDto>> { }

    public class GetLeaveRequestByIdQuery : IRequest<LeaveRequestDto?> 
    {
        public int Id { get; set; }
        public GetLeaveRequestByIdQuery(int id) => Id = id;
    }

    public class LeaveRequestQueryHandler : 
        IRequestHandler<GetAllLeaveRequestsQuery, List<LeaveRequestDto>>,
        IRequestHandler<GetLeaveRequestByIdQuery, LeaveRequestDto?>
    {
        private readonly AppDbContext _context;

        public LeaveRequestQueryHandler(AppDbContext context) => _context = context;

        public async Task<List<LeaveRequestDto>> Handle(GetAllLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _context.LeaveRequests
                .AsNoTracking() // Performance optimization for Read-only operations
                .Select(l => new LeaveRequestDto {
                    Id = l.Id,
                    EmployeeId = l.EmployeeId,
                    LeaveType = l.LeaveType,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Status = l.Status.ToString(), // Converts Enum to string for UI
                    Reason = l.Reason,
                    CreatedAt = l.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<LeaveRequestDto?> Handle(GetLeaveRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.LeaveRequests
                .AsNoTracking() // Performance optimization
                .Where(l => l.Id == request.Id)
                .Select(l => new LeaveRequestDto {
                    Id = l.Id,
                    EmployeeId = l.EmployeeId,
                    LeaveType = l.LeaveType,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Status = l.Status.ToString(), // Converts Enum to string
                    Reason = l.Reason,
                    CreatedAt = l.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}