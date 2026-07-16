using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.API.Models
{
    public enum ApprovalStatus 
    { 
        Pending = 0,
        Approved = 1,
        Rejected = 2 
    }
    
    public class Document
    {
        [Key]
        public int Id { get; set; }

        public int UploadedByUserId { get; set; }
        
        public int EmployeeId { get; set; }

        [Required, StringLength(200)]
        public string DocumentName { get; set; } = string.Empty;

        [StringLength(100)]
        public string FileCategory { get; set; } = string.Empty;

        [Required]
        public string S3StoragePath { get; set; } = string.Empty;

        [Required]
        public ApprovalStatus Approval { get; set; } = ApprovalStatus.Pending;

        // Corrected property for Soft Delete
        public bool IsActive { get; set; } = true;

        [StringLength(255)]
        public string FileHash { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}