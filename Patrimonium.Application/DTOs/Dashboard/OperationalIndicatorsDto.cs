namespace Patrimonium.Application.DTOs.Dashboard
{
    public record OperationalIndicatorsDto(
        int ActiveProperties,   // Número de propriedades ativas (alugadas ou disponíveis para aluguel)
        int VacantProperties,   // Número de propriedades vagas (disponíveis para aluguel)
        int OpenMaintenances    // Número de manutenções em aberto (pendentes ou em andamento)
    );
}
