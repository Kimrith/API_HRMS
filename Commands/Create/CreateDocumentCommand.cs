using HRMS.API.Models;
using HRMS.API.DTOs;
using HRMS.API.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRMS.API.Commands
{
    // The Command
    public record CreateDocumentCommand(UploadDocumentDto Dto, IFormFile File, int CurrentUserId) : IRequest<int>;

    // The Handler
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateDocumentCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            // 1. Define local storage path
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            // 2. Generate unique filename (Guid + Original Filename)
            var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 3. Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            // 4. Map to Model
            var document = new Document
            {
                EmployeeId = request.Dto.EmployeeId,
                UploadedByUserId = request.CurrentUserId, // Set from command
                DocumentName = request.Dto.DocumentName,
                FileCategory = request.Dto.FileCategory,
                S3StoragePath = filePath,
                Approval = ApprovalStatus.Pending, // Explicitly set default
                IsActive = true,
                UploadDate = DateTime.UtcNow
            };

            // 5. Save to Database
            _context.Documents.Add(document);
            await _context.SaveChangesAsync(cancellationToken);

            return document.Id;
        }
    }
}