using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Maintenance;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    [Authorize]
    public class MaintenancesController : ControllerBase
    {
        private readonly MaintenanceService _service;

        public MaintenancesController(MaintenanceService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceDto dto)
        {
            await _service.Create(UserId, dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var result = await _service.GetMy(UserId);
            return Ok(result);
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            await _service.Start(id, UserId);
            return Ok();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id, [FromQuery] decimal realCost)
        {
            await _service.Complete(id, UserId, realCost);
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _service.Cancel(id, UserId);
            return Ok();
        }
    }
}
