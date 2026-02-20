namespace Patrimonium.Application.DTOs.Response
{
    public class AnomalyDetectionResult
    {
        public IReadOnlyCollection<AnomalyPoint> Anomalies { get; set; } = [];
    }

    public class AnomalyPoint
    {
        public string Period { get; set; } = "";
        public string Metric { get; set; } = "";
        public decimal Value { get; set; }
        public decimal Expected { get; set; }
        public string Severity { get; set; } = ""; // low, medium, high
    }
}
