using Patrimonium.Application.DTOs.Inspections;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class InspectionService
    {
        private readonly IInspectionRepository _repo;

        public InspectionService(IInspectionRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateInspectionDto dto)
        {
            var inspection = new Inspection(
                dto.PropertyId,
                dto.ContractId,
                dto.Type,
                dto.Report,
                dto.ScoreCondition,
                dto.InspectionDate
            );

            await _repo.Add(inspection);
            await _repo.SaveChanges();
        }

        public Task<List<Inspection>> GetByProperty(Guid propertyId)
            => _repo.GetByProperty(propertyId);
    }
}