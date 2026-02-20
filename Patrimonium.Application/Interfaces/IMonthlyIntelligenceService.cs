using Patrimonium.Application.DTOs.Input;
using Patrimonium.Application.DTOs.Response;

namespace Patrimonium.Application.Interfaces
{
    public interface IMonthlyIntelligenceService
    {
        Task<MonthlyComparisonResult> CompareAsync(Guid userId, int y1, int m1, int y2, int m2);
        Task<TrendAnalysisResult> AnalyzeTrendsAsync(Guid userId, int lastNMonths);
        Task<AnomalyDetectionResult> DetectAnomaliesAsync(Guid userId, int lastNMonths);
        Task<ProjectionResult> ProjectAsync(Guid userId, int monthsAhead);
        Task<SimulationResult> SimulateAsync(Guid userId, SimulationInput input);
    }
}
