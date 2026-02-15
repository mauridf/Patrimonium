using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class FinancialTransaction : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid PropertyId { get; private set; }
        public Guid? ContractId { get; private set; }

        public TransactionType Type { get; private set; }
        public TransactionCategory Category { get; private set; }

        public string Description { get; private set; } = null!;
        public decimal Amount { get; private set; }

        public DateTime TransactionDate { get; private set; }
        public DateTime CompetenceMonth { get; private set; }

        public bool IsPaid { get; private set; }

        protected FinancialTransaction() { }

        public FinancialTransaction(
            Guid userId,
            Guid propertyId,
            Guid? contractId,
            TransactionType type,
            TransactionCategory category,
            string description,
            decimal amount,
            DateTime transactionDate,
            DateTime competenceMonth,
            bool isPaid
        )
        {
            UserId = userId;
            PropertyId = propertyId;
            ContractId = contractId;
            Type = type;
            Category = category;
            Description = description;
            Amount = amount;
            TransactionDate = transactionDate;
            CompetenceMonth = competenceMonth;
            IsPaid = isPaid;
        }

        public void MarkAsPaid()
        {
            IsPaid = true;
            MarkUpdated();
        }
    }
}
