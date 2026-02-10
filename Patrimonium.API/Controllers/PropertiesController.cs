using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Property;
using Patrimonium.Application.UseCases.Properties;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/properties")]
    [Authorize]
    public class PropertiesController : ControllerBase
    {
        private readonly ICreatePropertyUseCase _createUseCase;

        public PropertiesController(ICreatePropertyUseCase createUseCase)
        {
            _createUseCase = createUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _createUseCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }
    }
}
