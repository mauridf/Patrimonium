namespace Patrimonium.Application.DTOs.Simulation
{
    public class SimulationRequestDto
    {
        public int Months { get; set; } = 12;

        // parâmetros de simulação
        public decimal RentAdjustmentPercent { get; set; } = 0;   // reajuste %
        public decimal VacancyRatePercent { get; set; } = 0;      // vacância %
        public decimal ExpenseGrowthPercent { get; set; } = 0;    // aumento de custos %
        public int NewProperties { get; set; } = 0;               // expansão
    }
}
