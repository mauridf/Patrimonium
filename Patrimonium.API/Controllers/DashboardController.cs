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

        public DashboardController(IDashboardQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int month, [FromQuery] int year)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var data = await _service.GetDashboardAsync(userId, month, year);
            return Ok(data);
        }
    }
}
