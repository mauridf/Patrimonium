namespace Patrimonium.Application.DTOs.Intelligence
{
    public class PropertyRankDto
    {
        public Guid PropertyId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public decimal Net { get; set; }
        public decimal ROI { get; set; }
        public int Score { get; set; }
    }
}
