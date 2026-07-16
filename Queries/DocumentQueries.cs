using HRMS.API.DTOs;
using HRMS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Data;

namespace HRMS.API.Queries
{
    public record GetDocumentByIdQuery(int Id) : IRequest<DocumentResponseDto?>;
    public record GetAllDocumentsQuery : IRequest<List<DocumentResponseDto>>;

    public class DocumentQueryHandler : 
        IRequestHandler<GetDocumentByIdQuery, DocumentResponseDto?>,
        IRequestHandler<GetAllDocumentsQuery, List<DocumentResponseDto>>
    {
        private readonly AppDbContext _context;

        public DocumentQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        // Get single document (Must be active)
        public async Task<DocumentResponseDto?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Documents
                .Where(d => d.Id == request.Id && d.IsActive)
                .Select(d => new DocumentResponseDto
                {
                    Id = d.Id,
                    UploadedByUserId = d.UploadedByUserId,
                    EmployeeId = d.EmployeeId,
                    DocumentName = d.DocumentName,
                    FileCategory = d.FileCategory,
                    S3StoragePath = d.S3StoragePath,
                    Approval = d.Approval,
                    IsActive = d.IsActive,
                    UploadDate = d.UploadDate
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        // Get all documents (Only shows active)
        public async Task<List<DocumentResponseDto>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Documents
                .Where(d => d.IsActive)
                .Select(d => new DocumentResponseDto
                {
                    Id = d.Id,
                    UploadedByUserId = d.UploadedByUserId,
                    EmployeeId = d.EmployeeId,
                    DocumentName = d.DocumentName,
                    FileCategory = d.FileCategory,
                    S3StoragePath = d.S3StoragePath,
                    Approval = d.Approval,
                    IsActive = d.IsActive,
                    UploadDate = d.UploadDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}