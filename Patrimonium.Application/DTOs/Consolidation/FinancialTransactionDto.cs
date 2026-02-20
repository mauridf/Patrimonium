using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Consolidation
{
    public class FinancialTransactionDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? ContractId { get; set; }

        public TransactionType Type { get; set; }
        public TransactionCategory Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
