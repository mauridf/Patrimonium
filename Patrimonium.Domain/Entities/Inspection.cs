using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Inspection : BaseEntity
    {
        public Guid PropertyId { get; private set; }
        public Guid? ContractId { get; private set; }

        public InspectionType Type { get; private set; }

        public string Report { get; private set; } = null!;
        public int ScoreCondition { get; private set; } // 0–100

        public DateTime InspectionDate { get; private set; }

        protected Inspection() { }

        public Inspection(
            Guid propertyId,
            Guid? contractId,
            InspectionType type,
            string report,
            int scoreCondition,
            DateTime inspectionDate
        )
        {
            PropertyId = propertyId;
            ContractId = contractId;
            Type = type;
            Report = report;
            ScoreCondition = scoreCondition;
            InspectionDate = inspectionDate;
        }

        public void UpdateReport(string report, int score)
        {
            Report = report;
            ScoreCondition = score;
            MarkUpdated();
        }
    }
}