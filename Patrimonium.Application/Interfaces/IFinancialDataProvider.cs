using Patrimonium.Application.DTOs.Consolidation;

namespace Patrimonium.Application.Interfaces
{
    public interface IFinancialDataProvider
    {
        Task<IReadOnlyCollection<FinancialTransactionDto>> GetTransactionsAsync(Guid userId, int year, int month);
    }
}
