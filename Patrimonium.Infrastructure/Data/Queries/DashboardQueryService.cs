using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.DTOs.Dashboard;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Enums;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Data.Queries
{
    public class DashboardQueryService : IDashboardQueryService
    {
        private readonly PatrimoniumDbContext _context;

        public DashboardQueryService(PatrimoniumDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync(Guid userId, int month, int year)
        {
            var txs = await _context.FinancialTransactions
                .AsNoTracking()
                .Where(t => t.UserId == userId &&
                            t.CompetenceMonth.Month == month &&
                            t.CompetenceMonth.Year == year)
                .ToListAsync();

            var income = txs.Where(t => t.Type == FinancialType.Income).Sum(t => t.Amount);
            var expense = txs.Where(t => t.Type != FinancialType.Income).Sum(t => t.Amount);
            var cashflow = txs.Where(t => t.IsPaid)
                              .Sum(t => t.Type == FinancialType.Income ? t.Amount : -t.Amount);

            var propertyCount = await _context.Properties.CountAsync(p => p.UserId == userId);

            return new DashboardDto
            {
                Financial = new FinancialIndicatorsDto
                {
                    MonthlyIncome = income,
                    MonthlyExpense = expense,
                    NetProfit = income - expense,
                    CashFlow = cashflow,
                    RoiMonth = propertyCount == 0 ? 0 : (income - expense) / propertyCount,
                    AvgTicketPerProperty = propertyCount == 0 ? 0 : income / propertyCount
                },
                Operational = new OperationalIndicatorsDto
                {
                    ActiveProperties = propertyCount,
                    VacantProperties = 0,
                    OpenMaintenances = 0
                },
                Patrimonial = new PatrimonialIndicatorsDto
                {
                    TotalEstimatedValue = 0,
                    Appreciation = 0
                }
            };
        }
    }
}
