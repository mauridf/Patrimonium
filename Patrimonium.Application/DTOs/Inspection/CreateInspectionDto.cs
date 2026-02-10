using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Inspection
{
    public class CreateInspectionDto
    {
        public Guid PropertyId { get; set; }
        public Guid? ContractId { get; set; }
        public InspectionType Type { get; set; }
        public string Report { get; set; } = default!;
        public int ScoreCondition { get; set; } // 0–100
        public DateTime InspectionDate { get; set; }
    }
}
