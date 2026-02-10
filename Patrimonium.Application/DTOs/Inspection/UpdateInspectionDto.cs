using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Inspection
{
    public class UpdateInspectionDto
    {
        public InspectionType Type { get; set; }
        public string Report { get; set; } = default!;
        public int ScoreCondition { get; set; }
        public DateTime InspectionDate { get; set; }
    }
}
