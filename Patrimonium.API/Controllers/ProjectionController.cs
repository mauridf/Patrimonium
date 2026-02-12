using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial/projection")]
    [Authorize]
    public class ProjectionController : ControllerBase
    {
        private readonly IProjectionEngine _engine;

        public ProjectionController(IProjectionEngine engine)
        {
            _engine = engine;
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> Project(Guid propertyId, [FromQuery] int months = 12)
            => Ok(await _engine.Project(propertyId, months));
    }

}
