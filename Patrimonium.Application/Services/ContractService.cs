using Patrimonium.Application.DTOs.Contracts;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class ContractService
    {
        private readonly IContractRepository _repo;

        public ContractService(IContractRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(Guid userId, CreateContractDto dto)
        {
            var contract = new Contract(
                userId,
                dto.PropertyId,
                dto.PersonId,
                dto.Type,
                dto.StartDate,
                dto.EndDate,
                dto.MonthlyValue,
                dto.DailyValue,
                dto.AdjustmentIndex,
                dto.AdjustmentPeriodMonths,
                dto.GuaranteeType,
                dto.FinePercentage,
                dto.Status
            );

            await _repo.Add(contract);
            await _repo.SaveChanges();
        }

        public Task<List<Contract>> GetMyContracts(Guid userId)
            => _repo.GetByUser(userId);
    }
}
