using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Inspection : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public Guid? ContractId { get; set; }
        public InspectionType Type { get; set; }
        public string Report { get; set; } = default!;
        public int ScoreCondition { get; set; }
        public DateTime InspectionDate { get; set; }

        public Property Property { get; set; } = default!;
    }
}
