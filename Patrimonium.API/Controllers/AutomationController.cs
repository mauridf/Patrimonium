using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.UseCases.Automation;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/automation")]
    [Authorize]
    public class AutomationController : ControllerBase
    {
        private readonly AutomationUseCase _useCase;

        public AutomationController(AutomationUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("run")]
        public async Task<IActionResult> Run()
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            await _useCase.RunAsync(userId);
            return Ok(new { status = "automation executed" });
        }
    }
}
