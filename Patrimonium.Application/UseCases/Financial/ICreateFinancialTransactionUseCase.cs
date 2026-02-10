using Patrimonium.Application.DTOs.Financial;

namespace Patrimonium.Application.UseCases.Financial
{
    public interface ICreateFinancialTransactionUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateFinancialTransactionDto dto);
    }
}
