using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Contracts;
using Patrimonium.Application.Services;
using System.Security.Claims;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    [Authorize]
    public class ContractsController : ControllerBase
    {
        private readonly ContractService _service;

        public ContractsController(ContractService service)
        {
            _service = service;
        }

        private Guid UserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost]
        public async Task<IActionResult> Create(CreateContractDto dto)
        {
            await _service.Create(UserId, dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            var result = await _service.GetMyContracts(UserId);
            return Ok(result);
        }
    }
}
