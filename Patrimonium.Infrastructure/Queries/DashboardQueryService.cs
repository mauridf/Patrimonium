using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.DTOs.Dashboard;
using Patrimonium.Application.Interfaces;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Queries
{
    public class DashboardQueryService : IDashboardQueryService
    {
        private readonly PatrimoniumDbContext _db;

        public DashboardQueryService(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardDto> GetDashboard(Guid userId, int month, int year)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            // Financeiro
            var incomes = await _db.FinancialTransactions
                .Where(x => x.UserId == userId &&
                            x.Type == Domain.Enums.TransactionType.Income &&
                            x.TransactionDate >= start &&
                            x.TransactionDate < end &&
                            !x.IsDeleted)
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

            var expenses = await _db.FinancialTransactions
                .Where(x => x.UserId == userId &&
                            x.Type == Domain.Enums.TransactionType.Expense &&
                            x.TransactionDate >= start &&
                            x.TransactionDate < end &&
                            !x.IsDeleted)
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

            var net = incomes - expenses;
            var roi = incomes > 0 ? (net / incomes) * 100 : 0;

            // Operacional
            var activeProps = await _db.Properties
                .CountAsync(x => x.UserId == userId && x.Status == "Active" && !x.IsDeleted);

            var vacantProps = await _db.Properties
                .CountAsync(x => x.UserId == userId && x.Status == "Vacant" && !x.IsDeleted);

            var openMaint = await _db.Maintenances
                .CountAsync(x => x.UserId == userId &&
                                 x.Status != Domain.Enums.MaintenanceStatus.Completed &&
                                 !x.IsDeleted);

            // Patrimonial
            var totalMaintenanceCost = await _db.Maintenances
                .Where(x => x.UserId == userId && x.RealCost != null && !x.IsDeleted)
                .SumAsync(x => (decimal?)x.RealCost!) ?? 0;

            // Valor estimado inicial (placeholder estratégico — depois vira IA/valuation engine)
            var estimatedValue = await _db.Properties
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .SumAsync(x => (decimal?)x.TotalAreaM2 * 5000) ?? 0;

            return new DashboardDto(
                new FinancialIndicatorsDto(
                    incomes,
                    expenses,
                    net,
                    net,
                    roi
                ),
                new OperationalIndicatorsDto(
                    activeProps,
                    vacantProps,
                    openMaint
                ),
                new PatrimonialIndicatorsDto(
                    estimatedValue,
                    totalMaintenanceCost
                )
            );
        }
    }
}