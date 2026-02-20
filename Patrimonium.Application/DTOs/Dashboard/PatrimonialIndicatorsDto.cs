namespace Patrimonium.Application.DTOs.Dashboard
{
    public record PatrimonialIndicatorsDto(
        decimal TotalEstimatedValue,    // Valor total estimado do patrimônio (soma dos valores de mercado das propriedades)
        decimal TotalMaintenanceCost    // Custo total de manutenção acumulado (soma dos custos de manutenção de todas as propriedades)
    );
}
