using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.DTOs;
using Patrimonium.Application.DTOs.Consolidation;
using Patrimonium.Application.Interfaces;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.MonthlyClosing.Providers
{
    public class FinancialDataProvider : IFinancialDataProvider
    {
        private readonly PatrimoniumDbContext _db;

        public FinancialDataProvider(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyCollection<FinancialTransactionDto>> GetTransactionsAsync(Guid userId, int year, int month)
        {
            return await _db.FinancialTransactions
                .Where(t => t.UserId == userId &&
                            t.TransactionDate.Year == year &&
                            t.TransactionDate.Month == month)
                .Select(t => new FinancialTransactionDto
                {
                    Id = t.Id,
                    PropertyId = t.PropertyId,
                    ContractId = t.ContractId,
                    Type = t.Type,
                    Category = t.Category,
                    Amount = t.Amount,
                    Date = t.TransactionDate
                }).ToListAsync();
        }
    }
}
