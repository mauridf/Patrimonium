using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Financial;
using Patrimonium.Application.UseCases.Financial;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial")]
    [Authorize]
    public class FinancialController : ControllerBase
    {
        private readonly ICreateFinancialTransactionUseCase _useCase;

        public FinancialController(ICreateFinancialTransactionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialTransactionDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }
    }
}
