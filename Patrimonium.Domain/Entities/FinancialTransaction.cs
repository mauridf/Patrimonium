using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class FinancialTransaction : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public Guid? ContractId { get; set; }

        public FinancialType Type { get; set; }
        public FinancialCategory Category { get; set; }

        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }      // data real
        public DateTime CompetenceMonth { get; set; }       // competência contábil
        public bool IsPaid { get; set; }

        // Navegação
        public Property Property { get; set; } = default!;
        public Contract? Contract { get; set; }
    }
}
