using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IFinancialTransactionRepository
    {
        Task Add(FinancialTransaction transaction);
        Task<List<FinancialTransaction>> GetByUser(Guid userId);
        Task<FinancialTransaction?> GetById(Guid id, Guid userId);
        Task SaveChanges();
    }
}
