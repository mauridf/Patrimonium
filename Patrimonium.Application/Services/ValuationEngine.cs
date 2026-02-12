using Patrimonium.Application.DTOs.Results;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Application.Services
{
    public class ValuationEngine : IValuationEngine
    {
        public async Task<PropertyValuation> Calculate(Guid propertyId)
        {
            // MVP lógico
            var income = 5000m * 12;
            var capRate = 0.08m;

            var capValue = income / capRate;

            var valuation = new PropertyValuation
            {
                PropertyId = propertyId,
                CurrentIncome = income,
                ProjectedIncome = income * 1.1m,
                CapRate = capRate,
                IncomeApproachValue = capValue,
                DcfValue = capValue * 1.05m,
                FinalEstimatedValue = (capValue + (capValue * 1.05m)) / 2
            };

            return await Task.FromResult(valuation);
        }
    }
}
