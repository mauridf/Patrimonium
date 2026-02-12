using Microsoft.EntityFrameworkCore;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Data.Queries
{
    public class ContractQueryService
    {
        private readonly PatrimoniumDbContext _ctx;

        public ContractQueryService(PatrimoniumDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Contract>> GetAll(Guid userId)
            => await _ctx.Contracts
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

        public async Task<List<Contract>> GetByProperty(Guid propertyId)
            => await _ctx.Contracts
                .Where(x => x.PropertyId == propertyId)
                .AsNoTracking()
                .ToListAsync();

        public async Task<List<Contract>> GetByPerson(Guid personId)
            => await _ctx.Contracts
                .Where(x => x.PersonId == personId)
                .AsNoTracking()
                .ToListAsync();
    }
}
