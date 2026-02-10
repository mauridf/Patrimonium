using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Services
{
    public class FinancialDomainService
    {
        public void Validate(FinancialTransaction tx)
        {
            if (tx.Amount <= 0)
                throw new Exception("Valor inválido");

            if (tx.CompetenceMonth == default)
                throw new Exception("Competência obrigatória");

            if (tx.TransactionDate == default)
                throw new Exception("Data da transação obrigatória");
        }

        public decimal CalculateNet(IEnumerable<FinancialTransaction> txs)
        {
            var income = txs.Where(t => t.Type == FinancialType.Income).Sum(t => t.Amount);
            var expense = txs.Where(t => t.Type != FinancialType.Income).Sum(t => t.Amount);
            return income - expense;
        }

        public decimal CalculateCashFlow(IEnumerable<FinancialTransaction> txs)
        {
            return txs.Where(t => t.IsPaid).Sum(t =>
                t.Type == FinancialType.Income ? t.Amount : -t.Amount);
        }
    }
}
