using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Inspection;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Inspections;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/inspections")]
    [Authorize]
    public class InspectionController : BaseCrudController<Inspection>
    {
        private readonly ICreateInspectionUseCase _useCase;
        private readonly InspectionCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Inspection> UseCase => _crudUseCase;

        public InspectionController(ICreateInspectionUseCase useCase, InspectionCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInspectionDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateInspectionDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Type = dto.Type;
            entity.Report = dto.Report;
            entity.ScoreCondition = dto.ScoreCondition;
            entity.InspectionDate = dto.InspectionDate;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
