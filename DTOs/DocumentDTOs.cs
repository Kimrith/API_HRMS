using System.ComponentModel.DataAnnotations;
using HRMS.API.Models; // Needed to reference ApprovalStatus
using System.Text.Json.Serialization;

namespace HRMS.API.DTOs
{
    // Used when returning document information to the frontend
    public class DocumentResponseDto
    {
        public int Id { get; set; }
        public int UploadedByUserId { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string FileCategory { get; set; } = string.Empty;
        public string S3StoragePath { get; set; } = string.Empty;
        [JsonPropertyName("approval")]
        public ApprovalStatus Approval { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        public DateTime UploadDate { get; set; }
    }

    // Used when uploading a new document
    public class UploadDocumentDto
    {
        [Required]
        public int EmployeeId { get; set; }
        
        [Required, StringLength(200)]
        public string DocumentName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string FileCategory { get; set; } = string.Empty;
        
        // Note: You generally don't need to include 'Approval' here
        // because new uploads should default to 'Pending' in your handler.
    }
}