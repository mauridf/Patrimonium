namespace Patrimonium.Application.DTOs.Results
{
    public class PropertyValuation
    {
        public Guid PropertyId { get; set; }

        public decimal CurrentIncome { get; set; }
        public decimal ProjectedIncome { get; set; }

        public decimal CapRate { get; set; }
        public decimal DcfValue { get; set; }
        public decimal IncomeApproachValue { get; set; }

        public decimal FinalEstimatedValue { get; set; }
    }
}
