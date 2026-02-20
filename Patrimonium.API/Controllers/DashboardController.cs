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
        private readonly IDashboardQueryService _query;

        public DashboardController(IDashboardQueryService query)
        {
            _query = query;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int month, [FromQuery] int year)
        {
            var result = await _query.GetDashboard(UserId, month, year);
            return Ok(result);
        }
    }
}
