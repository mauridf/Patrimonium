using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class FinancialTransactionRepository : IFinancialTransactionRepository
    {
        private readonly PatrimoniumDbContext _db;

        public FinancialTransactionRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(FinancialTransaction transaction)
            => await _db.FinancialTransactions.AddAsync(transaction);

        public async Task<List<FinancialTransaction>> GetByUser(Guid userId)
            => await _db.FinancialTransactions
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task<FinancialTransaction?> GetById(Guid id, Guid userId)
            => await _db.FinancialTransactions
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}
