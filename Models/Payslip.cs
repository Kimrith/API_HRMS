using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.API.Models
{
    public enum PayslipStatus 
    {
        Paid,
        Pending, 
        Cancelled
    }

    public class Payslip
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeProfile Employee { get; set; } = null!;

        public DateTime PayDate { get; set; }
        public string PayPeriod { get; set; } = string.Empty; 

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Allowances { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal OvertimePay { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossPay { get; set; } 
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductions { get; set; } 
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPay { get; set; }

        public PayslipStatus Status { get; set; } 
    }
}