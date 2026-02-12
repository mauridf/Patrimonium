using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial/valuation")]
    [Authorize]
    public class ValuationController : ControllerBase
    {
        private readonly IValuationEngine _engine;

        public ValuationController(IValuationEngine engine)
        {
            _engine = engine;
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> Valuate(Guid propertyId)
            => Ok(await _engine.Calculate(propertyId));
    }
}
