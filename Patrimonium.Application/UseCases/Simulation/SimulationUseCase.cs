using Patrimonium.Application.DTOs.Simulation;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Simulation
{
    public class SimulationUseCase : ISimulationUseCase
    {
        private readonly IRepository<FinancialTransaction> _financialRepo;
        private readonly IRepository<Property> _propertyRepo;

        public SimulationUseCase(
            IRepository<FinancialTransaction> financialRepo,
            IRepository<Property> propertyRepo)
        {
            _financialRepo = financialRepo;
            _propertyRepo = propertyRepo;
        }

        public async Task<SimulationResultDto> SimulateAsync(Guid userId, SimulationRequestDto request, string scenario)
        {
            var financials = await _financialRepo.GetAllAsync(x => x.UserId == userId);
            var properties = await _propertyRepo.GetAllAsync(x => x.UserId == userId);

            var baseIncome = financials
                .Where(x => x.Type == FinancialType.Income)
                .Sum(x => x.Amount);

            var baseExpense = financials
                .Where(x => x.Type == FinancialType.Expense)
                .Sum(x => x.Amount);

            decimal incomeMultiplier = 1;
            decimal expenseMultiplier = 1;

            // 🎯 CENÁRIOS
            if (scenario == "otimista")
            {
                incomeMultiplier = 1.15m;
                expenseMultiplier = 1.05m;
            }
            else if (scenario == "pessimista")
            {
                incomeMultiplier = 0.85m;
                expenseMultiplier = 1.15m;
            }
            else // realista
            {
                incomeMultiplier = 1.05m;
                expenseMultiplier = 1.08m;
            }

            // reajuste
            var adjustedIncome = baseIncome * (1 + request.RentAdjustmentPercent / 100);

            // vacância
            var vacancyLoss = adjustedIncome * (request.VacancyRatePercent / 100);
            adjustedIncome -= vacancyLoss;

            // expansão
            var avgIncomePerProperty = properties.Any()
                ? baseIncome / properties.Count()
                : 0;

            var expansionIncome = avgIncomePerProperty * request.NewProperties;

            var projectedIncome = (adjustedIncome + expansionIncome) * incomeMultiplier;
            var projectedExpense = baseExpense * (1 + request.ExpenseGrowthPercent / 100) * expenseMultiplier;

            var net = projectedIncome - projectedExpense;
            var roi = projectedIncome == 0 ? 0 : (net / projectedIncome) * 100;

            return new SimulationResultDto
            {
                ProjectedIncome = projectedIncome,
                ProjectedExpense = projectedExpense,
                ProjectedNetProfit = net,
                ROI = roi,
                Scenario = scenario
            };
        }
    }
}
