using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Automation
{
    public class AutomationUseCase
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IRepository<FinancialTransaction> _financialRepo;
        private readonly IAlertService _alertService;
        private readonly IGovernanceService _governance;

        public AutomationUseCase(
            IRepository<Property> propertyRepo,
            IRepository<FinancialTransaction> financialRepo,
            IAlertService alertService,
            IGovernanceService governance)
        {
            _propertyRepo = propertyRepo;
            _financialRepo = financialRepo;
            _alertService = alertService;
            _governance = governance;
        }

        public async Task RunAsync(Guid userId)
        {
            var properties = await _propertyRepo.GetAllAsync(x => x.UserId == userId);
            var financials = await _financialRepo.GetAllAsync(x => x.UserId == userId);

            foreach (var prop in properties)
            {
                var propFin = financials.Where(x => x.PropertyId == prop.Id);

                var income = propFin
                    .Where(x => x.Type == FinancialType.Income)
                    .Sum(x => x.Amount);

                var expense = propFin
                    .Where(x => x.Type == FinancialType.Expense)
                    .Sum(x => x.Amount);

                if (expense > income)
                {
                    // alerta
                    await _alertService.CreateAlertAsync(new Alert
                    {
                        UserId = userId,
                        Title = "Imóvel em prejuízo",
                        Message = $"O imóvel {prop.InternalName} está operando com prejuízo.",
                        Type = "FINANCIAL",
                        Severity = "HIGH"
                    });

                    // governança
                    await _governance.LogEventAsync(new SystemEvent
                    {
                        UserId = userId,
                        Type = "ALERT",
                        Source = "AUTOMATION",
                        Description = $"Imóvel {prop.InternalName} em prejuízo financeiro",
                        Severity = "HIGH"
                    });
                }
            }
        }
    }
}
