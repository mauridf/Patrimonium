using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Contract;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Contract;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    [Authorize]
    public class ContractController : BaseCrudController<Contract>
    {
        private readonly ICreateContractUseCase _useCase;
        private readonly ContractCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Contract> UseCase => _crudUseCase;

        public ContractController(ICreateContractUseCase useCase, ContractCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContractDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateContractDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Type = dto.Type;
            entity.Status = dto.Status;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.MonthlyValue = dto.MonthlyValue;
            entity.DailyValue = dto.DailyValue;
            entity.AdjustmentIndex = dto.AdjustmentIndex;
            entity.AdjustmentPeriodMonths = dto.AdjustmentPeriodMonths;
            entity.GuaranteeType = dto.GuaranteeType;
            entity.FinePercentage = dto.FinePercentage;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
