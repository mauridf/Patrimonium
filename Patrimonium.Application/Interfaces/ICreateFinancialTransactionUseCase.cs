using Patrimonium.Application.DTOs.Financial;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateFinancialTransactionUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateFinancialTransactionDto dto);
    }
}
