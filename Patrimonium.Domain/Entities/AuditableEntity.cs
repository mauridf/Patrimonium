namespace Patrimonium.Domain.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; } = default!;
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
    }
}
