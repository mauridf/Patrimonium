namespace Patrimonium.Application.DTOs.Results
{
    public class PropertyProjectionResult
    {
        public Guid PropertyId { get; set; }
        public int Months { get; set; }

        public decimal ExpectedRevenue { get; set; }
        public decimal ExpectedCosts { get; set; }
        public decimal ExpectedProfit { get; set; }

        public decimal VacancyRate { get; set; }
        public decimal ROI { get; set; }
        public decimal Yield { get; set; }

        public List<MonthlyProjection> Monthly { get; set; } = new();
    }
}
