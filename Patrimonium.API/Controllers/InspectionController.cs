using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Inspection;
using Patrimonium.Application.Interfaces;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/inspections")]
    [Authorize]
    public class InspectionController : ControllerBase
    {
        private readonly ICreateInspectionUseCase _useCase;

        public InspectionController(ICreateInspectionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInspectionDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }
    }
}
