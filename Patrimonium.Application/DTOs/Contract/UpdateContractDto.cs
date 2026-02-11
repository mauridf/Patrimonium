using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Contract
{
    public class UpdateContractDto
    {
        public ContractType Type { get; set; }
        public ContractStatus Status { get; set; }

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
