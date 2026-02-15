using Patrimonium.Application.DTOs.Financial;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class FinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _repo;

        public FinancialTransactionService(IFinancialTransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(Guid userId, CreateFinancialTransactionDto dto)
        {
            var transaction = new FinancialTransaction(
                userId,
                dto.PropertyId,
                dto.ContractId,
                dto.Type,
                dto.Category,
                dto.Description,
                dto.Amount,
                dto.TransactionDate,
                dto.CompetenceMonth,
                dto.IsPaid
            );

            await _repo.Add(transaction);
            await _repo.SaveChanges();
        }

        public Task<List<FinancialTransaction>> GetMyTransactions(Guid userId)
            => _repo.GetByUser(userId);
    }
}
