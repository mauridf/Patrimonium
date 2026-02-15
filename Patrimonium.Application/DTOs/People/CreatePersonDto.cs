using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.People
{
    public record CreatePersonDto(
    string Name,
    PersonType Type,
    string? CpfCnpj,
    string? Email,
    string? Phone,
    string? Profession,
    decimal? IncomeEstimation,
    int? ScoreInternal,
    string? Notes
);
}
