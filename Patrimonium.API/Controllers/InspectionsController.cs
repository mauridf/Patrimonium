using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Inspections;
using Patrimonium.Application.Services;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/inspections")]
    [Authorize]
    public class InspectionsController : ControllerBase
    {
        private readonly InspectionService _service;

        public InspectionsController(InspectionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInspectionDto dto)
        {
            await _service.Create(dto);
            return Ok();
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetByProperty(Guid propertyId)
        {
            var result = await _service.GetByProperty(propertyId);
            return Ok(result);
        }
    }
}
