namespace Patrimonium.Domain.Entities.Snapshot
{
    public class FinancialSnapshot
    {
        public decimal TotalIncome { get; }
        public decimal TotalExpense { get; }
        public decimal TotalTax { get; }
        public decimal TotalMaintenance { get; }
        public decimal NetResult { get; }
        public decimal CashFlow { get; }

        private FinancialSnapshot() { }

        public FinancialSnapshot(
            decimal income,
            decimal expense,
            decimal tax,
            decimal maintenance)
        {
            TotalIncome = income;
            TotalExpense = expense;
            TotalTax = tax;
            TotalMaintenance = maintenance;
            NetResult = income - (expense + tax + maintenance);
            CashFlow = NetResult;
        }
    }
}
