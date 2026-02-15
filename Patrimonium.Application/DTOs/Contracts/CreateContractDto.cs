using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Contracts
{
    public record CreateContractDto(
    Guid PropertyId,
    Guid PersonId,
    ContractType Type,
    DateTime StartDate,
    DateTime EndDate,
    decimal? MonthlyValue,
    decimal? DailyValue,
    string? AdjustmentIndex,
    int AdjustmentPeriodMonths,
    GuaranteeType GuaranteeType,
    decimal FinePercentage,
    ContractStatus Status
);
}
