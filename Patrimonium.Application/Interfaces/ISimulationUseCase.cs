using Patrimonium.Application.DTOs.Simulation;

namespace Patrimonium.Application.Interfaces
{
    public interface ISimulationUseCase
    {
        Task<SimulationResultDto> SimulateAsync(Guid userId, SimulationRequestDto request, string scenario);
    }
}
