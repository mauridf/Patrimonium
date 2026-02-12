using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Billing : AuditableEntity
    {
        public Guid ContractId { get; set; }

        public DateTime ReferenceDate { get; set; } // mês/competência
        public decimal Amount { get; set; }

        public BillingType Type { get; set; } // Rent, Fine, Adjustment, Vacancy, Tax, Other
        public BillingStatus Status { get; set; } // Pending, Paid, Overdue, Canceled

        public DateTime DueDate { get; set; }
        public DateTime? PaidAt { get; set; }

        public Contract Contract { get; set; } = default!;
    }

}
