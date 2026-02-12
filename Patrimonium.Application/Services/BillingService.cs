using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.Services
{
    public class BillingService : IBillingService
    {
        private readonly IRepository<Billing> _repo;

        public BillingService(IRepository<Billing> repo)
        {
            _repo = repo;
        }

        public async Task<List<Billing>> GetAll(Guid userId)
            => await _repo.Query()
                .Include(x => x.Contract)
                .ThenInclude(c => c.Property)
                .ThenInclude(p => p.User)
                .Where(x => x.Contract.Property.User.Id == userId)
                .AsNoTracking()
                .ToListAsync();

        public async Task<List<Billing>> GetByContract(Guid contractId)
            => await _repo.Query()
                .Where(x => x.ContractId == contractId)
                .AsNoTracking()
                .ToListAsync();

        public async Task Pay(Guid billingId)
        {
            var b = await _repo.GetByIdAsync(billingId);
            if (b == null) throw new Exception("Billing not found");

            b.Status = Domain.Enums.BillingStatus.Paid;
            b.PaidAt = DateTime.UtcNow;

            _repo.Update(b);
        }
    }

}
