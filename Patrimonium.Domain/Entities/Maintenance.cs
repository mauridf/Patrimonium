using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Maintenance : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Priority { get; set; }
        public MaintenanceStatus Status { get; set; }
        public decimal? CostEstimation { get; set; }
        public decimal? RealCost { get; set; }
        public Guid? ResponsiblePersonId { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public Property Property { get; set; } = default!;
        public Person? ResponsiblePerson { get; set; }
    }
}
