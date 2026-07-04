namespace HRMS.API.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int UploadedByUserId { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string FileCategory { get; set; } = string.Empty;
        public string S3StoragePath { get; set; } = string.Empty;
        public string FileHash { get; set; } = string.Empty;
    }
}