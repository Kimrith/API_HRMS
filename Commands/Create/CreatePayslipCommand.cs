using HRMS.API.Data;
using HRMS.API.DTOs;
using HRMS.API.Models;

namespace HRMS.API.Commands.Create
{
    public class CreatePayslipCommand
    {
        private readonly AppDbContext _context;
        public CreatePayslipCommand(AppDbContext context) => _context = context;

        public async Task<int> ExecuteAsync(PayslipCreateDto dto)
        {
            // 1. Update the Employee's official salary in their profile
            var employee = await _context.EmployeeProfile.FindAsync(dto.EmployeeId);
            if (employee != null)
            {
                employee.BasicSalary = dto.BasicSalary;
                _context.EmployeeProfile.Update(employee);
            }

            // 2. Calculate values for the Payslip snapshot
            var gross = dto.BasicSalary + dto.Allowances + dto.OvertimePay;
            var net = gross - dto.Deductions;

            var payslip = new Payslip {
                EmployeeId = dto.EmployeeId,
                PayPeriod = dto.PayPeriod,
                PayDate = DateTime.UtcNow,
                BasicSalary = dto.BasicSalary,
                Allowances = dto.Allowances,
                OvertimePay = dto.OvertimePay,
                GrossPay = gross,
                Deductions = dto.Deductions,
                NetPay = net,
                Status = PayslipStatus.Pending
            };

            _context.Payslips.Add(payslip);
            
            // 3. Save both changes (Salary update + New Payslip) at once
            await _context.SaveChangesAsync();
            
            return payslip.Id;
        }
    }
}