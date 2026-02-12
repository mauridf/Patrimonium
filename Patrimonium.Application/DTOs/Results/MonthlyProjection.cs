namespace Patrimonium.Application.DTOs.Results
{
    public class MonthlyProjection
    {
        public DateTime Reference { get; set; }
        public decimal Revenue { get; set; }
        public decimal Costs { get; set; }
        public decimal Profit { get; set; }
    }
}
