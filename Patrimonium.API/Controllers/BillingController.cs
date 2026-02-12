using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial/billings")]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _service;

        public BillingController(IBillingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            return Ok(await _service.GetAll(userId));
        }

        [HttpGet("contract/{contractId}")]
        public async Task<IActionResult> ByContract(Guid contractId)
            => Ok(await _service.GetByContract(contractId));

        [HttpPut("{id}/pay")]
        public async Task<IActionResult> Pay(Guid id)
        {
            await _service.Pay(id);
            return NoContent();
        }
    }
}
