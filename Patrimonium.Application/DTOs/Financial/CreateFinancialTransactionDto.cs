using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Financial
{
    public record CreateFinancialTransactionDto(
    Guid PropertyId,
    Guid? ContractId,
    TransactionType Type,
    TransactionCategory Category,
    string Description,
    decimal Amount,
    DateTime TransactionDate,
    DateTime CompetenceMonth,
    bool IsPaid
);
}
