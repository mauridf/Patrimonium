using Patrimonium.Application.DTOs.Results;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Application.Services
{
    public class ProjectionEngine : IProjectionEngine
    {
        public async Task<PropertyProjectionResult> Project(Guid propertyId, int months)
        {
            // MVP lógico (sem dados externos ainda)
            var result = new PropertyProjectionResult
            {
                PropertyId = propertyId,
                Months = months
            };

            for (int i = 0; i < months; i++)
            {
                result.Monthly.Add(new MonthlyProjection
                {
                    Reference = DateTime.UtcNow.AddMonths(i),
                    Revenue = 5000,
                    Costs = 1200,
                    Profit = 3800
                });
            }

            result.ExpectedRevenue = result.Monthly.Sum(x => x.Revenue);
            result.ExpectedCosts = result.Monthly.Sum(x => x.Costs);
            result.ExpectedProfit = result.Monthly.Sum(x => x.Profit);

            result.VacancyRate = 0.08m;
            result.ROI = 0.15m;
            result.Yield = 0.09m;

            return await Task.FromResult(result);
        }
    }
}
