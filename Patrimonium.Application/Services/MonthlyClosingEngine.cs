using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.ValueObjects;

namespace Patrimonium.Application.Services
{
    public class MonthlyClosingEngine : IMonthlyClosingEngine
    {
        private readonly IFinancialDataProvider _financialProvider;
        private readonly IPropertyDataProvider _propertyProvider;
        private readonly IOperationalDataProvider _operationalProvider;
        private readonly IHashService _hashService;
        private readonly IMonthlyClosingRepository _repository;

        public MonthlyClosingEngine(
            IFinancialDataProvider financialProvider,
            IPropertyDataProvider propertyProvider,
            IOperationalDataProvider operationalProvider,
            IHashService hashService,
            IMonthlyClosingRepository repository)
        {
            _financialProvider = financialProvider;
            _propertyProvider = propertyProvider;
            _operationalProvider = operationalProvider;
            _hashService = hashService;
            _repository = repository;
        }

        public async Task<MonthlyClosing> GenerateAsync(Guid userId, int year, int month)
        {
            var period = new ClosingPeriod(year, month);

            // 1. Carregar dados
            var transactions = await _financialProvider.GetTransactionsAsync(userId, year, month);
            var properties = await _propertyProvider.GetPropertiesAsync(userId);
            var operational = await _operationalProvider.GetOperationalDataAsync(userId, year, month);

            // 2. Consolidação financeira
            var income = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            var tax = transactions.Where(t => t.Type == "Tax").Sum(t => t.Amount);
            var maintenance = transactions.Where(t => t.Type == "Maintenance").Sum(t => t.Amount);

            var financialSnapshot = new FinancialSnapshot(income, expense, tax, maintenance);

            // 3. Operacional
            var operationalSnapshot = new OperationalSnapshot(
                operational.ActiveProperties,
                operational.VacantProperties,
                operational.ActiveContracts,
                operational.OpenMaintenances
            );

            // 4. Patrimonial
            var totalEstimated = properties.Sum(p => p.EstimatedValue);
            var totalInvested = properties.Sum(p => p.InvestedValue);

            var patrimonialSnapshot = new PatrimonialSnapshot(
                totalEstimated,
                totalInvested,
                roiAccumulated: totalInvested == 0 ? 0 : (financialSnapshot.NetResult / totalInvested)
            );

            // 5. Transaction Snapshots
            var txSnapshots = transactions.Select(t =>
                new TransactionSnapshot(
                    t.Id,
                    t.PropertyId,
                    t.ContractId,
                    t.Type,
                    t.Category,
                    t.Amount,
                    t.Date
                )
            ).ToList();

            // 6. Property Snapshots
            var propSnapshots = properties.Select(p =>
            {
                var propTx = transactions.Where(t => t.PropertyId == p.Id);
                var propIncome = propTx.Where(t => t.Type == "Income").Sum(t => t.Amount);
                var propExpense = propTx.Where(t => t.Type != "Income").Sum(t => t.Amount);

                return new PropertySnapshot(
                    p.Id,
                    p.InternalName,
                    p.Type,
                    p.Purpose,
                    propIncome,
                    propExpense
                );
            }).ToList();

            // 7. Snapshot mensal
            var snapshot = new MonthlySnapshot(
                financialSnapshot,
                operationalSnapshot,
                patrimonialSnapshot,
                txSnapshots,
                propSnapshots
            );

            // 8. Hash de integridade
            var hash = _hashService.GenerateHash(snapshot);

            // 9. Fechamento
            var closing = new MonthlyClosing(userId, period, snapshot, hash);

            // 10. Persistência
            await _repository.SaveAsync(closing);

            return closing;
        }
    }
}
