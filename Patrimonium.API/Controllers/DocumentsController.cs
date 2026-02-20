using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Documents;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _service;

        public DocumentsController(DocumentService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] CreateDocumentDto dto,
            IFormFile file
        )
        {
            using var stream = file.OpenReadStream();

            await _service.Upload(
                UserId,
                dto,
                stream,
                file.FileName,
                file.ContentType
            );

            return Ok();
        }
    }
}
