using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Simulation;
using Patrimonium.Application.Interfaces;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/simulation")]
    [Authorize]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationUseCase _useCase;

        public SimulationController(ISimulationUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("{scenario}")]
        public async Task<IActionResult> Simulate(string scenario, [FromBody] SimulationRequestDto request)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var result = await _useCase.SimulateAsync(userId, request, scenario);
            return Ok(result);
        }
    }
}
