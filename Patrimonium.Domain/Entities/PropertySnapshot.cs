namespace Patrimonium.Domain.Entities
{
    public class PropertySnapshot
    {
        public Guid PropertyId { get; }
        public string InternalName { get; }
        public string Type { get; }
        public string Purpose { get; }

        public decimal Income { get; }
        public decimal Expense { get; }
        public decimal NetResult { get; }

        public PropertySnapshot(
            Guid propertyId,
            string internalName,
            string type,
            string purpose,
            decimal income,
            decimal expense)
        {
            PropertyId = propertyId;
            InternalName = internalName;
            Type = type;
            Purpose = purpose;
            Income = income;
            Expense = expense;
            NetResult = income - expense;
        }
    }
}
