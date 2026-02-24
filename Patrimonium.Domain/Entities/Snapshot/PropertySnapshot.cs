using Microsoft.EntityFrameworkCore;

namespace Patrimonium.Domain.Entities.Snapshot
{
    [Owned]
    public class PropertySnapshot
    {
        public Guid PropertyId { get; }
        public string InternalName { get; }
        public string Type { get; }
        public string Purpose { get; }

        public decimal Income { get; }
        public decimal Expense { get; }
        public decimal NetResult { get; }

        private PropertySnapshot() { }

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
