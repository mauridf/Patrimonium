namespace Patrimonium.Application.DTOs.Dashboard
{
    public record DashboardDto(
        FinancialIndicatorsDto Financial,       // Indicadores financeiros (renda, despesas, lucro, fluxo de caixa, ROI)
        OperationalIndicatorsDto Operational,   // Indicadores operacionais (propriedades ativas, vagas, manutenções em aberto)
        PatrimonialIndicatorsDto Patrimonial    // Indicadores patrimoniais (valor estimado do patrimônio, custo de manutenção acumulado)
    );
}
