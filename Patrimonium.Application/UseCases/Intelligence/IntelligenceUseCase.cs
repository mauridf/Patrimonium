using Patrimonium.Application.DTOs.Intelligence;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Intelligence
{
    public class IntelligenceUseCase : IIntelligenceUseCase
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IRepository<FinancialTransaction> _financialRepo;

        public IntelligenceUseCase(
            IRepository<Property> propertyRepo,
            IRepository<FinancialTransaction> financialRepo)
        {
            _propertyRepo = propertyRepo;
            _financialRepo = financialRepo;
        }

        public async Task<IntelligenceSummaryDto> AnalyzeAsync(Guid userId)
        {
            var properties = await _propertyRepo.GetAllAsync(x => x.UserId == userId);
            var financials = await _financialRepo.GetAllAsync(x => x.UserId == userId);

            var ranking = new List<PropertyRankDto>();

            foreach (var prop in properties)
            {
                var propFinancials = financials.Where(x => x.PropertyId == prop.Id);

                var income = propFinancials
                    .Where(x => x.Type == FinancialType.Income)
                    .Sum(x => x.Amount);

                var expense = propFinancials
                    .Where(x => x.Type == FinancialType.Expense)
                    .Sum(x => x.Amount);

                var net = income - expense;
                var roi = income == 0 ? 0 : (net / income) * 100;

                var score = 0;
                if (roi > 30) score += 40;
                if (net > 0) score += 30;
                if (expense < income * 0.4m) score += 30;

                ranking.Add(new PropertyRankDto
                {
                    PropertyId = prop.Id,
                    Name = prop.InternalName,
                    Income = income,
                    Expense = expense,
                    Net = net,
                    ROI = roi,
                    Score = score
                });
            }

            var ordered = ranking.OrderByDescending(x => x.Score).ToList();

            var best = ordered.FirstOrDefault();
            var worst = ordered.LastOrDefault();

            var financialBottlenecks = new List<string>();
            var operationalBottlenecks = new List<string>();
            var recommendations = new List<string>();

            if (ranking.Any(x => x.ROI < 10))
                financialBottlenecks.Add("Baixo ROI em parte do patrimônio");

            if (ranking.Any(x => x.Expense > x.Income))
                financialBottlenecks.Add("Imóveis operando no prejuízo");

            if (ranking.Count(x => x.Net <= 0) > 0)
                recommendations.Add("Desinvestir ou reestruturar imóveis deficitários");

            if (ranking.Any(x => x.ROI > 40))
                recommendations.Add("Reinvestir em imóveis de alta performance");

            var patrimonialScore = ordered.Any()
                ? (int)ordered.Average(x => x.Score)
                : 0;

            return new IntelligenceSummaryDto
            {
                BestProperty = best!,
                WorstProperty = worst!,
                Ranking = ordered,
                FinancialBottlenecks = financialBottlenecks,
                OperationalBottlenecks = operationalBottlenecks,
                Recommendations = recommendations,
                PatrimonialScore = patrimonialScore
            };
        }
    }
}
