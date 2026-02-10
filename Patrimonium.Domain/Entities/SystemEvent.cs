namespace Patrimonium.Domain.Entities
{
    public class SystemEvent : AuditableEntity
    {
        public Guid UserId { get; set; }
        public string Type { get; set; } = default!;   // ALERT, RULE, DECISION, AUTOMATION
        public string Source { get; set; } = default!; // dashboard, intelligence, simulation, contract...
        public string Description { get; set; } = default!;
        public string Severity { get; set; } = default!; // LOW, MEDIUM, HIGH, CRITICAL
    }
}
