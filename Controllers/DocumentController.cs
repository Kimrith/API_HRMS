using HRMS.API.Commands;
using HRMS.API.DTOs;
using HRMS.API.Models;
using HRMS.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HRMS.API.Data;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] 
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/document/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentDto dto, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var command = new CreateDocumentCommand(dto, file, userId);
            var result = await _mediator.Send(command);

            return Ok(new { DocumentId = result, Message = "Document uploaded successfully." });
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            // 1. Get the document record
            var document = await _mediator.Send(new GetDocumentByIdQuery(id));
            if (document == null) return NotFound();

            // 2. Validate existence
            if (!System.IO.File.Exists(document.S3StoragePath)) 
                return NotFound("File not found on server.");

            // 3. Open the file stream
            // Ensure 'FileShare.Read' is used so other processes can still read the file
            var stream = new FileStream(document.S3StoragePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            // 4. Return the FileStreamResult
            // ASP.NET Core will automatically dispose of the stream when the request finishes
            return File(stream, "application/pdf", Path.GetFileName(document.S3StoragePath));
        }

        // GET: api/document
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _mediator.Send(new GetAllDocumentsQuery());
            return Ok(documents);
        }

        // GET: api/document/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _mediator.Send(new GetDocumentByIdQuery(id));
            if (document == null) return NotFound("Document not found.");
            return Ok(document);
        }

        // PATCH: api/document/{id}/approve
        [HttpPatch("{id}/approve")]
        // [Authorize(Roles = "HR,Admin")]
        public async Task<IActionResult> UpdateApproval(int id, [FromBody] ApprovalStatus status)
        {
            var command = new UpdateDocumentApprovalCommand(id, status);
            var result = await _mediator.Send(command);

            if (!result) return NotFound(new { Message = "Document not found." });

            return Ok(new { Message = $"Document status updated to {status}." });
        }
    }
}