using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patrimonium.Application.DTOs.Maintenance;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Maintenance;
using Patrimonium.Domain.Entities;

namespace Patrimonium.API.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    [Authorize]
    public class MaintenanceController : BaseCrudController<Maintenance>
    {
        private readonly ICreateMaintenanceUseCase _useCase;
        private readonly MaintenanceCrudUseCase _crudUseCase;

        protected override BaseCrudUseCase<Maintenance> UseCase => _crudUseCase;

        public MaintenanceController(ICreateMaintenanceUseCase useCase, MaintenanceCrudUseCase crudUseCase)
        {
            _useCase = useCase;
            _crudUseCase = crudUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue("userId")!);
            var id = await _useCase.ExecuteAsync(userId, dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateMaintenanceDto dto)
        {
            var entity = await UseCase.GetById(id);
            if (entity == null) return NotFound();

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.Status = dto.Status;
            entity.Priority = dto.Priority;
            entity.RealCost = dto.RealCost;
            entity.CostEstimation = dto.CostEstimation;
            entity.ResponsiblePersonId = dto.ResponsiblePersonId;
            entity.ClosedAt = dto.ClosedAt;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = User.FindFirstValue("userId");

            await UseCase.Update(entity);
            return NoContent();
        }
    }
}
