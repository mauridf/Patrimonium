using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly PatrimoniumDbContext _db;

        public ContractRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Contract contract)
            => await _db.Contracts.AddAsync(contract);

        public async Task<List<Contract>> GetByUser(Guid userId)
            => await _db.Contracts
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task<Contract?> GetById(Guid id, Guid userId)
            => await _db.Contracts
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}
