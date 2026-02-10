using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Services
{
    public class FinancialEngineService
    {
        public FinancialTransaction GenerateMonthlyRent(Contract contract, DateTime competence)
        {
            return new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                UserId = contract.UserId,
                PropertyId = contract.PropertyId,
                ContractId = contract.Id,
                Type = FinancialType.Income,
                Category = FinancialCategory.Rent,
                Description = "Receita automática de aluguel",
                Amount = contract.MonthlyValue,
                TransactionDate = competence,
                CompetenceMonth = new DateTime(competence.Year, competence.Month, 1),
                IsPaid = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public FinancialTransaction GenerateFine(Contract contract, decimal value)
        {
            return new FinancialTransaction
            {
                Id = Guid.NewGuid(),
                UserId = contract.UserId,
                PropertyId = contract.PropertyId,
                ContractId = contract.Id,
                Type = FinancialType.Income,
                Category = FinancialCategory.Fine,
                Description = "Multa contratual",
                Amount = value,
                TransactionDate = DateTime.UtcNow,
                CompetenceMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1),
                IsPaid = false,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
