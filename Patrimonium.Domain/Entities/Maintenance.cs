using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Maintenance : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid PropertyId { get; private set; }

        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public MaintenancePriority Priority { get; private set; }
        public MaintenanceStatus Status { get; private set; }

        public decimal CostEstimation { get; private set; }
        public decimal? RealCost { get; private set; }

        public Guid? ResponsiblePersonId { get; private set; }

        public DateTime OpenedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        protected Maintenance() { }

        public Maintenance(
            Guid userId,
            Guid propertyId,
            string title,
            string description,
            MaintenancePriority priority,
            decimal costEstimation,
            Guid? responsiblePersonId
        )
        {
            UserId = userId;
            PropertyId = propertyId;
            Title = title;
            Description = description;
            Priority = priority;
            Status = MaintenanceStatus.Open;
            CostEstimation = costEstimation;
            ResponsiblePersonId = responsiblePersonId;
            OpenedAt = DateTime.UtcNow;
        }

        public void Start()
        {
            Status = MaintenanceStatus.InProgress;
            MarkUpdated();
        }

        public void Complete(decimal realCost)
        {
            Status = MaintenanceStatus.Completed;
            RealCost = realCost;
            ClosedAt = DateTime.UtcNow;
            MarkUpdated();
        }

        public void Cancel()
        {
            Status = MaintenanceStatus.Cancelled;
            ClosedAt = DateTime.UtcNow;
            MarkUpdated();
        }
    }
}