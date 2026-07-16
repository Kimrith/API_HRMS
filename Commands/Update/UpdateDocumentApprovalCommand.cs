using HRMS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HRMS.API.Data;

namespace HRMS.API.Commands
{
    // The Command
    public record UpdateDocumentApprovalCommand(int DocumentId, ApprovalStatus NewStatus) : IRequest<bool>;

    // The Handler
    public class UpdateDocumentApprovalCommandHandler : IRequestHandler<UpdateDocumentApprovalCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateDocumentApprovalCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateDocumentApprovalCommand request, CancellationToken cancellationToken)
        {
            // Find the document
            var document = await _context.Documents.FindAsync(request.DocumentId);

            // Ensure it exists AND is currently active (Soft Delete Check)
            if (document == null || !document.IsActive) return false;

            // Update the status
            document.Approval = request.NewStatus;

            // Save changes
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}