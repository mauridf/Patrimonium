namespace Patrimonium.Application.DTOs.Dashboard
{
    public record FinancialIndicatorsDto(
        decimal MonthlyIncome,  // Renda mensal total (aluguel, etc.)
        decimal MonthlyExpense, // Despesas mensais totais (manutenção, impostos, etc.)
        decimal NetProfit,      // Lucro líquido mensal (renda - despesas)
        decimal CashFlow,       // Fluxo de caixa mensal (entradas - saídas)
        decimal RoiMonth        // Retorno sobre investimento mensal (lucro líquido / valor investido)
    );
}
