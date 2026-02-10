using Patrimonium.Domain.Interfaces;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PatrimoniumDbContext _context;

        public UnitOfWork(PatrimoniumDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}
