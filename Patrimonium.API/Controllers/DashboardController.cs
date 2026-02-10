using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardQueryService _service;
        private readonly IDashboardUseCase _useCase;

        public DashboardController(IDashboardQueryService service, IDashboardUseCase useCase)
        {
            _service = service;
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int month, [FromQuery] int year)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var data = await _service.GetDashboardAsync(userId, month, year);
            return Ok(data);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var data = await _useCase.GetSummaryAsync(userId);
            return Ok(data);
        }
    }
}
