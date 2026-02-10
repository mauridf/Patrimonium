namespace Patrimonium.Domain.Entities
{
    public class Alert : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Type { get; set; } = default!;     // FINANCIAL, OPERATIONAL, LEGAL, STRATEGIC
        public string Severity { get; set; } = default!; // LOW, MEDIUM, HIGH, CRITICAL
        public bool IsRead { get; set; } = false;
    }
}
