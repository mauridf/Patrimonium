using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Inspections
{
    public record CreateInspectionDto(
        Guid PropertyId,
        Guid? ContractId,
        InspectionType Type,
        string Report,
        int ScoreCondition,
        DateTime InspectionDate
    );
}
