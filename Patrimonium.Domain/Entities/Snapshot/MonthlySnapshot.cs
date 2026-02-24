using Microsoft.EntityFrameworkCore;

namespace Patrimonium.Domain.Entities.Snapshot
{
    [Owned]
    public class MonthlySnapshot
    {
        public Guid Id { get; private set; }

        public FinancialSnapshot Financial { get; private set; }
        public OperationalSnapshot Operational { get; private set; }
        public PatrimonialSnapshot Patrimonial { get; private set; }

        public IReadOnlyCollection<TransactionSnapshot> Transactions { get; private set; }
        public IReadOnlyCollection<PropertySnapshot> Properties { get; private set; }

        private MonthlySnapshot() { }

        public MonthlySnapshot(
            FinancialSnapshot financial,
            OperationalSnapshot operational,
            PatrimonialSnapshot patrimonial,
            IEnumerable<TransactionSnapshot> transactions,
            IEnumerable<PropertySnapshot> properties)
        {
            Id = Guid.NewGuid();
            Financial = financial;
            Operational = operational;
            Patrimonial = patrimonial;
            Transactions = transactions.ToList().AsReadOnly();
            Properties = properties.ToList().AsReadOnly();
        }
    }
}
