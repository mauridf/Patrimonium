using Patrimonium.Application.DTOs.Financial;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Services;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Application.UseCases.Financial
{
    public class CreateFinancialTransactionUseCase : ICreateFinancialTransactionUseCase
    {
        private readonly IRepository<FinancialTransaction> _repo;
        private readonly IUnitOfWork _uow;
        private readonly FinancialDomainService _domainService;

        public CreateFinancialTransactionUseCase(
            IRepository<FinancialTransaction> repo,
            IUnitOfWork uow,
            FinancialDomainService domainService)
        {
            _repo = repo;
            _uow = uow;
            _domainService = domainService;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateFinancialTransactionDto dto)
        {
            var tx = new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PropertyId = dto.PropertyId,
                ContractId = dto.ContractId,
                Type = dto.Type,
                Category = dto.Category,
                Description = dto.Description,
                Amount = dto.Amount,
                TransactionDate = dto.TransactionDate,
                CompetenceMonth = dto.CompetenceMonth,
                IsPaid = dto.IsPaid,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domainService.Validate(tx);

            await _repo.AddAsync(tx);
            await _uow.CommitAsync();

            return tx.Id;
        }
    }
}
