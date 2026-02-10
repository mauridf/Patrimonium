using System.Runtime.InteropServices.Marshalling;
using Patrimonium.Application.DTOs.Dashboard;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Dashboard
{
    public class DashboardUseCase : IDashboardUseCase
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IRepository<Patrimonium.Domain.Entities.Contract> _contractRepo;
        private readonly IRepository<FinancialTransaction> _financialRepo;

        public DashboardUseCase(
            IRepository<Property> propertyRepo,
            IRepository<Patrimonium.Domain.Entities.Contract> contractRepo,
            IRepository<FinancialTransaction> financialRepo)
        {
            _propertyRepo = propertyRepo;
            _contractRepo = contractRepo;
            _financialRepo = financialRepo;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync(Guid userId)
        {
            var properties = await _propertyRepo.GetAllAsync(x => x.UserId == userId);
            var contracts = await _contractRepo.GetAllAsync(x => x.UserId == userId);
            var financials = await _financialRepo.GetAllAsync(x => x.UserId == userId);

            var totalIncome = financials.Where(x => x.Type == Domain.Enums.FinancialType.Income).Sum(x => x.Amount);
            var totalExpense = financials.Where(x => x.Type == Domain.Enums.FinancialType.Expense).Sum(x => x.Amount);

            var activeContracts = contracts.Count(x => x.Status == Domain.Enums.ContractStatus.Active);
            var expiredContracts = contracts.Count(x => x.Status == Domain.Enums.ContractStatus.Expired);

            var occupied = activeContracts;
            var totalProperties = properties.Count();
            var vacant = totalProperties - occupied;

            var avgRent = contracts.Any() ? contracts.Average(x => x.MonthlyValue) : 0;

            var occupancyRate = totalProperties == 0 ? 0 : (decimal)occupied / totalProperties * 100;
            var vacancyRate = 100 - occupancyRate;

            var roi = totalIncome == 0 ? 0 : ((totalIncome - totalExpense) / totalIncome) * 100;

            return new DashboardSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetBalance = totalIncome - totalExpense,

                TotalProperties = totalProperties,
                OccupiedProperties = occupied,
                VacantProperties = vacant,

                ActiveContracts = activeContracts,
                ExpiredContracts = expiredContracts,

                AverageRent = avgRent,
                OccupancyRate = occupancyRate,
                VacancyRate = vacancyRate,
                ROI = roi
            };
        }
    }
}
