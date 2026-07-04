namespace HRMS.API.Models
{
    public class Payslip
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string PayPeriod { get; set; } = string.Empty;
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        public string Status { get; set; } = "Issued";
    }
}