using HRMS.API.Data;
using HRMS.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Queries
{
    public class PayslipQueries
    {
        private readonly AppDbContext _context;
        public PayslipQueries(AppDbContext context) => _context = context;

        public async Task<List<PayslipReadDto>> GetAllAsync() =>
            await _context.Payslips
                .Include(p => p.Employee)
                .Select(p => new PayslipReadDto {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId,
                    EmployeeFullName = $"{p.Employee.FirstName} {p.Employee.LastName}",
                    PayDate = p.PayDate,
                    PayPeriod = p.PayPeriod,
                    BasicSalary = p.BasicSalary,
                    Allowances = p.Allowances,
                    OvertimePay = p.OvertimePay,
                    GrossPay = p.GrossPay,
                    Deductions = p.Deductions,
                    NetPay = p.NetPay,
                    Status = p.Status
                }).ToListAsync();
    }
}