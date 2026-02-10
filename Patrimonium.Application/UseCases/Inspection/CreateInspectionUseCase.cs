using Patrimonium.Application.DTOs.Inspection;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;

namespace Patrimonium.Application.UseCases.Inspections
{
    public class CreateInspectionUseCase : ICreateInspectionUseCase
    {
        private readonly IRepository<Domain.Entities.Inspection> _repo;
        private readonly IUnitOfWork _uow;
        private readonly InspectionDomainService _domain;

        public CreateInspectionUseCase(
            IRepository<Domain.Entities.Inspection> repo,
            IUnitOfWork uow,
            InspectionDomainService domain)
        {
            _repo = repo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateInspectionDto dto)
        {
            var inspection = new Domain.Entities.Inspection
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PropertyId = dto.PropertyId,
                ContractId = dto.ContractId,
                Type = dto.Type,
                Report = dto.Report,
                ScoreCondition = dto.ScoreCondition,
                InspectionDate = dto.InspectionDate,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domain.Validate(inspection);

            await _repo.AddAsync(inspection);
            await _uow.CommitAsync();

            return inspection.Id;
        }
    }
}
