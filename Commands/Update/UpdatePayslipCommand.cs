using HRMS.API.Data;
using HRMS.API.DTOs;

namespace HRMS.API.Commands.Update
{
    public class UpdatePayslipCommand
    {
        private readonly AppDbContext _context;
        public UpdatePayslipCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id, PayslipUpdateStatusDto dto)
        {
            var payslip = await _context.Payslips.FindAsync(id);
            if (payslip == null) return false;

            payslip.Status = dto.Status;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}