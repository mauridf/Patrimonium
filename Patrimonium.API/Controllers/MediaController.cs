using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Media;
using Patrimonium.Application.Services;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/media")]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly MediaService _service;

        public MediaController(MediaService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] CreateMediaDto dto,
            IFormFile file
        )
        {
            using var stream = file.OpenReadStream();

            await _service.Upload(
                dto,
                stream,
                file.FileName,
                file.ContentType
            );

            return Ok();
        }
    }
}
