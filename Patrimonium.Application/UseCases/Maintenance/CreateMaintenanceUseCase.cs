using Patrimonium.Application.DTOs.Maintenance;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;

namespace Patrimonium.Application.UseCases.Maintenance
{
    public class CreateMaintenanceUseCase : ICreateMaintenanceUseCase
    {
        private readonly IRepository<Domain.Entities.Maintenance> _repo;
        private readonly IUnitOfWork _uow;
        private readonly MaintenanceDomainService _domain;

        public CreateMaintenanceUseCase(
            IRepository<Domain.Entities.Maintenance> repo,
            IUnitOfWork uow,
            MaintenanceDomainService domain)
        {
            _repo = repo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateMaintenanceDto dto)
        {
            var m = new Domain.Entities.Maintenance
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PropertyId = dto.PropertyId,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                CostEstimation = dto.CostEstimation,
                RealCost = dto.RealCost,
                ResponsiblePersonId = dto.ResponsiblePersonId,
                OpenedAt = dto.OpenedAt,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domain.Validate(m);

            await _repo.AddAsync(m);
            await _uow.CommitAsync();

            return m.Id;
        }
    }
}
