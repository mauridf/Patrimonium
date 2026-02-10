namespace Patrimonium.Application.DTOs.Simulation
{
    public class SimulationResultDto
    {
        public decimal ProjectedIncome { get; set; }
        public decimal ProjectedExpense { get; set; }
        public decimal ProjectedNetProfit { get; set; }

        public decimal ROI { get; set; }

        public string Scenario { get; set; } = default!;
    }
}
