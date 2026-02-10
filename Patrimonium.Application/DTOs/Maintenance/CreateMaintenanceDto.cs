using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Maintenance
{
    public class CreateMaintenanceDto
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Priority { get; set; } // 1=Baixa | 2=Média | 3=Alta | 4=Crítica
        public MaintenanceStatus Status { get; set; } // Open, InProgress, Done, Cancelled
        public decimal? CostEstimation { get; set; }
        public decimal? RealCost { get; set; }
        public Guid? ResponsiblePersonId { get; set; }
        public DateTime OpenedAt { get; set; }
    }
}
