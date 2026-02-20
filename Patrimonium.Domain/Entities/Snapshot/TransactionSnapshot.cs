namespace Patrimonium.Domain.Entities.Snapshot
{
    public class TransactionSnapshot
    {
        public Guid TransactionId { get; }
        public Guid PropertyId { get; }
        public Guid? ContractId { get; }

        public string Type { get; }
        public string Category { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }

        public TransactionSnapshot(
            Guid transactionId,
            Guid propertyId,
            Guid? contractId,
            string type,
            string category,
            decimal amount,
            DateTime date)
        {
            TransactionId = transactionId;
            PropertyId = propertyId;
            ContractId = contractId;
            Type = type;
            Category = category;
            Amount = amount;
            Date = date;
        }
    }
}
