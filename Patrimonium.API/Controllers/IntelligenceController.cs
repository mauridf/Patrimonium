using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/intelligence")]
    [Authorize]
    public class IntelligenceController : ControllerBase
    {
        private readonly IIntelligenceUseCase _useCase;

        public IntelligenceController(IIntelligenceUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var result = await _useCase.AnalyzeAsync(userId);
            return Ok(result);
        }
    }
}
