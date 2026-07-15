using System;
using HRMS.API.Models;

namespace HRMS.API.DTOs
{
    // Used for retrieving payslip information (includes Employee name)
    public class PayslipReadDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; } = string.Empty;
        public DateTime PayDate { get; set; }
        public string PayPeriod { get; set; } = string.Empty;
        
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        
        public PayslipStatus Status { get; set; }
    }

    // Used when an admin creates a new payslip
    public class PayslipCreateDto
    {
        public int EmployeeId { get; set; }
        public string PayPeriod { get; set; } = string.Empty;
        
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
        
        // Note: GrossPay and NetPay are usually calculated 
        // by the server, not passed by the client.
    }

    // Used for updating the status (e.g., changing from Pending to Paid)
    public class PayslipUpdateStatusDto
    {
        public PayslipStatus Status { get; set; }
    }
}