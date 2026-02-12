using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Contract;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Contract;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;
using Patrimonium.Infrastructure.Data.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    [Authorize]
    public class ContractController : BaseCrudController<Contract>
    {
        private readonly ICreateContractUseCase _useCase;
        private readonly ContractCrudUseCase _crudUseCase;
        private readonly IContractLifecycleUseCase _lifecycle;
        private readonly ContractQueryService _query;

        protected override BaseCrudUseCase<Contract> UseCase => _crudUseCase;

        public ContractController(ICreateContractUseCase useCase, ContractCrudUseCase crudUseCase, IContractLifecycleUseCase lifecycle, ContractQueryService query)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
            _lifecycle = lifecycle;
            _query = query;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            return Ok(await _query.GetAll(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var list = await _query.GetAll(Guid.Parse(User.FindFirstValue("userId")!));
            var c = list.FirstOrDefault(x => x.Id == id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> ByProperty(Guid propertyId)
            => Ok(await _query.GetByProperty(propertyId));

        [HttpGet("person/{personId}")]
        public async Task<IActionResult> ByPerson(Guid personId)
            => Ok(await _query.GetByPerson(personId));

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _lifecycle.Activate(id);
            return NoContent();
        }

        [HttpPut("{id}/suspend")]
        public async Task<IActionResult> Suspend(Guid id)
        {
            await _lifecycle.Suspend(id);
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _lifecycle.Cancel(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish(Guid id)
        {
            await _lifecycle.Finish(id);
            return NoContent();
        }
    }
}
