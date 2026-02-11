using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Patrimonium.Application.DTOs.Financial;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Financial;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/financial")]
    [Authorize]
    public class FinancialController : BaseCrudController<FinancialTransaction>
    {
        private readonly ICreateFinancialTransactionUseCase _useCase;
        private readonly FinancialTransactionCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<FinancialTransaction> UseCase => _crudUseCase;

        public FinancialController(ICreateFinancialTransactionUseCase useCase, FinancialTransactionCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFinancialTransactionDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateFinancialDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Type = dto.Type;
            entity.Category = dto.Category;
            entity.Description = dto.Description;
            entity.Amount = dto.Amount;
            entity.TransactionDate = dto.TransactionDate;
            entity.CompetenceMonth = dto.CompetenceMonth;
            entity.IsPaid = dto.IsPaid;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
