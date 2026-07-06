using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Data;
using HRMS.API.DTOs;

namespace HRMS.API.Queries
{
    // The Request
    public class GetAllUsersQuery : IRequest<List<UserWithProfileDto>> { }
    public record GetUserByIdQuery(int UserId) : IRequest<UserWithProfileDto?>;

    // The Handler
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserWithProfileDto>>
    {
        private readonly AppDbContext _context;

        public GetAllUsersQueryHandler(AppDbContext context) => _context = context;

        public async Task<List<UserWithProfileDto>> Handle(GetAllUsersQuery request, CancellationToken ct)
        {
            return await _context.User
                .Include(u => u.EmployeeProfile)
                .Select(u => new UserWithProfileDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role.ToString(),
                    Status = u.Status.ToString(),
                    CreatedAt = u.CreatedAt,
                    EmployeeProfile = u.EmployeeProfile != null ? new EmployeeProfileResponseDto
                    {
                        Id = u.EmployeeProfile.Id,
                        FirstName = u.EmployeeProfile.FirstName,
                        LastName = u.EmployeeProfile.LastName,
                        Level = u.EmployeeProfile.Level.ToString(),
                        Department = u.EmployeeProfile.Department,
                        PhoneNumber = u.EmployeeProfile.PhoneNumber,
                        BasicSalary = u.EmployeeProfile.BasicSalary
                        // Add other fields as needed
                    } : null
                })
                .ToListAsync(ct);
        }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserWithProfileDto?>
        {
            private readonly AppDbContext _context;

            public GetUserByIdQueryHandler(AppDbContext context) => _context = context;

            public async Task<UserWithProfileDto?> Handle(GetUserByIdQuery request, CancellationToken ct)
            {
                var user = await _context.User
                    .Include(u => u.EmployeeProfile)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

                if (user == null) return null;

                return new UserWithProfileDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    Status = user.Status.ToString(),
                    CreatedAt = user.CreatedAt,
                    EmployeeProfile = user.EmployeeProfile != null ? new EmployeeProfileResponseDto
                    {
                        Id = user.EmployeeProfile.Id,
                        FirstName = user.EmployeeProfile.FirstName,
                        LastName = user.EmployeeProfile.LastName,
                        Level = user.EmployeeProfile.Level.ToString(),
                        Department = user.EmployeeProfile.Department,
                        PhoneNumber = user.EmployeeProfile.PhoneNumber,
                        BasicSalary = user.EmployeeProfile.BasicSalary
                    } : null
                };
            }
        }
    }
}