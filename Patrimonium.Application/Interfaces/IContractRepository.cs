using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IContractRepository
    {
        Task Add(Contract contract);
        Task<List<Contract>> GetByUser(Guid userId);
        Task<Contract?> GetById(Guid id, Guid userId);
        Task SaveChanges();
    }
}
