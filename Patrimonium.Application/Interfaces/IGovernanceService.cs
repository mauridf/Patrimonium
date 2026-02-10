using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IGovernanceService
    {
        Task LogEventAsync(SystemEvent systemEvent);
    }
}
