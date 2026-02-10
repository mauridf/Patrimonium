using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Maintenance;
using Patrimonium.Application.Interfaces;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        private readonly ICreateMaintenanceUseCase _useCase;

        public MaintenanceController(ICreateMaintenanceUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }
    }
}
