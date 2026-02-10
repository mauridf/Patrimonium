namespace Patrimonium.Application.DTOs.Dashboard
{
    public class FinancialIndicatorsDto
    {
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpense { get; set; }
        public decimal NetProfit { get; set; }
        public decimal CashFlow { get; set; }
        public decimal RoiMonth { get; set; }
        public decimal AvgTicketPerProperty { get; set; }
    }
}
