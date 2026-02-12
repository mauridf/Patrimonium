namespace Patrimonium.Domain.Entities
{
    public class Vacancy : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal EstimatedLoss { get; set; }
    }
}
