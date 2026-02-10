using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Contract : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public Guid PersonId { get; set; } // Inquilino / Comprador

        public ContractType Type { get; set; }
        public ContractStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal MonthlyValue { get; set; }
        public decimal? DailyValue { get; set; }

        public AdjustmentIndex AdjustmentIndex { get; set; }
        public int AdjustmentPeriodMonths { get; set; }

        public GuaranteeType GuaranteeType { get; set; }

        public decimal FinePercentage { get; set; } // multa contratual

        // Relacionamentos
        public Property Property { get; set; } = default!;
        public Person Person { get; set; } = default!;
    }
}
