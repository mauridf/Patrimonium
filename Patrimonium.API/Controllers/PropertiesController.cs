using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Property;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/properties")]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertiesController(PropertyService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {
            await _service.Create(UserId, dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var result = await _service.GetMyProperties(UserId);
            return Ok(result);
        }
    }
}
