using HRMS.API.Data;

namespace HRMS.API.Commands.Delete
{
    public class DeletePayslipCommand
    {
        private readonly AppDbContext _context;
        public DeletePayslipCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id)
        {
            var payslip = await _context.Payslips.FindAsync(id);
            if (payslip == null) return false;

            _context.Payslips.Remove(payslip);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}