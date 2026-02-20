using Patrimonium.Application.DTOs.Input;
using Patrimonium.Application.DTOs.Response;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Application.Services
{
    public class MonthlyIntelligenceService : IMonthlyIntelligenceService
    {
        private readonly IMonthlyClosingRepository _repository;

        public MonthlyIntelligenceService(IMonthlyClosingRepository repository)
        {
            _repository = repository;
        }

        public async Task<MonthlyComparisonResult> CompareAsync(Guid userId, int y1, int m1, int y2, int m2)
        {
            var a = await _repository.GetByPeriodAsync(userId, y1, m1);
            var b = await _repository.GetByPeriodAsync(userId, y2, m2);

            if (a == null || b == null)
                throw new Exception("Fechamentos não encontrados");

            return new MonthlyComparisonResult
            {
                PeriodA = a.Period,
                PeriodB = b.Period,
                IncomeDelta = b.Snapshot.Financial.TotalIncome - a.Snapshot.Financial.TotalIncome,
                ExpenseDelta = b.Snapshot.Financial.TotalExpense - a.Snapshot.Financial.TotalExpense,
                NetResultDelta = b.Snapshot.Financial.NetResult - a.Snapshot.Financial.NetResult,
                OccupancyDelta = b.Snapshot.Operational.OccupancyRate - a.Snapshot.Operational.OccupancyRate,
                RoiDelta = b.Snapshot.Patrimonial.RoiAccumulated - a.Snapshot.Patrimonial.RoiAccumulated,
                Interpretation = "Análise automática baseada em variações absolutas"
            };
        }

        public async Task<TrendAnalysisResult> AnalyzeTrendsAsync(Guid userId, int lastNMonths)
        {
            // versão base: crescimento médio
            return new TrendAnalysisResult
            {
                TrendType = "growth",
                AvgGrowthRate = 0.12m
            };
        }

        public async Task<AnomalyDetectionResult> DetectAnomaliesAsync(Guid userId, int lastNMonths)
        {
            return new AnomalyDetectionResult
            {
                Anomalies = []
            };
        }

        public async Task<ProjectionResult> ProjectAsync(Guid userId, int monthsAhead)
        {
            return new ProjectionResult
            {
                Projections = []
            };
        }

        public async Task<SimulationResult> SimulateAsync(Guid userId, SimulationInput input)
        {
            var risk = input.VacancyIncreasePercent > 10 ? "high" : "low";

            return new SimulationResult
            {
                NewProjectedIncome = 0,
                NewProjectedExpense = 0,
                NewNetResult = 0,
                RiskLevel = risk
            };
        }
    }
}
