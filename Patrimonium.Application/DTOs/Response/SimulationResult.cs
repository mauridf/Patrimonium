namespace Patrimonium.Application.DTOs.Response
{
    public class SimulationResult
    {
        public decimal NewProjectedIncome { get; set; }
        public decimal NewProjectedExpense { get; set; }
        public decimal NewNetResult { get; set; }
        public string RiskLevel { get; set; } = "";
    }
}
