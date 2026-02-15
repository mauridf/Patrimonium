using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid PropertyId { get; private set; }
        public Guid PersonId { get; private set; }

        public ContractType Type { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public decimal? MonthlyValue { get; private set; }
        public decimal? DailyValue { get; private set; }

        public string? AdjustmentIndex { get; private set; }
        public int AdjustmentPeriodMonths { get; private set; }

        public GuaranteeType GuaranteeType { get; private set; }
        public decimal FinePercentage { get; private set; }

        public ContractStatus Status { get; private set; }

        protected Contract() { }

        public Contract(
            Guid userId,
            Guid propertyId,
            Guid personId,
            ContractType type,
            DateTime startDate,
            DateTime endDate,
            decimal? monthlyValue,
            decimal? dailyValue,
            string? adjustmentIndex,
            int adjustmentPeriodMonths,
            GuaranteeType guaranteeType,
            decimal finePercentage,
            ContractStatus status
        )
        {
            UserId = userId;
            PropertyId = propertyId;
            PersonId = personId;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            MonthlyValue = monthlyValue;
            DailyValue = dailyValue;
            AdjustmentIndex = adjustmentIndex;
            AdjustmentPeriodMonths = adjustmentPeriodMonths;
            GuaranteeType = guaranteeType;
            FinePercentage = finePercentage;
            Status = status;
        }

        public void UpdateStatus(ContractStatus status)
        {
            Status = status;
            MarkUpdated();
        }
    }
}
