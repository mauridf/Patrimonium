namespace Patrimonium.Application.DTOs.Response
{
    public class TrendAnalysisResult
    {
        public string TrendType { get; set; } = ""; // growth, decline, stable
        public decimal AvgGrowthRate { get; set; }
        public IReadOnlyCollection<TrendPoint> Points { get; set; } = [];
    }

    public class TrendPoint
    {
        public string Period { get; set; } = "";
        public decimal NetResult { get; set; }
    }
}
