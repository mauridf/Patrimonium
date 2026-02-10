using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Contract
{
    public class CreateContractDto
    {
        public Guid PropertyId { get; set; }
        public Guid PersonId { get; set; }

        public ContractType Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal MonthlyValue { get; set; }
        public decimal? DailyValue { get; set; }

        public AdjustmentIndex AdjustmentIndex { get; set; }
        public int AdjustmentPeriodMonths { get; set; }

        public GuaranteeType GuaranteeType { get; set; }
        public decimal FinePercentage { get; set; }
    }
}
