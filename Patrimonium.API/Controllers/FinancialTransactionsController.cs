using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Financial;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial-transactions")]
    [Authorize]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly FinancialTransactionService _service;

        public FinancialTransactionsController(FinancialTransactionService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialTransactionDto dto)
        {
            await _service.Create(UserId, dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var result = await _service.GetMyTransactions(UserId);
            return Ok(result);
        }
    }
}
