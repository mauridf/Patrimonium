using Patrimonium.Application.DTOs.Contract;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;
using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.UseCases.Contract
{
    public class CreateContractUseCase : ICreateContractUseCase
    {
        private readonly IRepository<Domain.Entities.Contract> _repo;
        private readonly IRepository<FinancialTransaction> _financialRepo;
        private readonly IUnitOfWork _uow;
        private readonly ContractDomainService _domain;

        public CreateContractUseCase(
            IRepository<Domain.Entities.Contract> repo,
                IRepository<FinancialTransaction> financialRepo,
            IUnitOfWork uow,
            ContractDomainService domain)
        {
            _repo = repo;
            _financialRepo = financialRepo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateContractDto dto)
        {
            var contract = new Domain.Entities.Contract
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PropertyId = dto.PropertyId,
                PersonId = dto.PersonId,
                Type = dto.Type,
                Status = ContractStatus.Active,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MonthlyValue = dto.MonthlyValue,
                DailyValue = dto.DailyValue,
                AdjustmentIndex = dto.AdjustmentIndex,
                AdjustmentPeriodMonths = dto.AdjustmentPeriodMonths,
                GuaranteeType = dto.GuaranteeType,
                FinePercentage = dto.FinePercentage,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domain.Validate(contract);

            await _repo.AddAsync(contract);
            await _uow.CommitAsync();

            // após salvar contrato
            var engine = new FinancialEngineService();
            var rent = engine.GenerateMonthlyRent(contract, DateTime.UtcNow);

            await _financialRepo.AddAsync(rent);
            await _uow.CommitAsync();

            return contract.Id;
        }
    }
}
