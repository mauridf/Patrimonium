namespace Patrimonium.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public FinancialIndicatorsDto Financial { get; set; } = new();
        public OperationalIndicatorsDto Operational { get; set; } = new();
        public PatrimonialIndicatorsDto Patrimonial { get; set; } = new();
    }
}
