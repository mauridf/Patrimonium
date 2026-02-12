using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IBillingService
    {
        Task<List<Billing>> GetAll(Guid userId);
        Task<List<Billing>> GetByContract(Guid contractId);
        Task Pay(Guid billingId);
    }
}
