using Patrimonium.Application.DTOs.Intelligence;

namespace Patrimonium.Application.Interfaces
{
    public interface IIntelligenceUseCase
    {
        Task<IntelligenceSummaryDto> AnalyzeAsync(Guid userId);
    }
}
