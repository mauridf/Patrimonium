using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Maintenance
{
    public record CreateMaintenanceDto(
        Guid PropertyId,
        string Title,
        string Description,
        MaintenancePriority Priority,
        decimal CostEstimation,
        Guid? ResponsiblePersonId
    );
}