using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.Services
{
    public class GovernanceService : IGovernanceService
    {
        private readonly IRepository<SystemEvent> _repo;

        public GovernanceService(IRepository<SystemEvent> repo)
        {
            _repo = repo;
        }

        public async Task LogEventAsync(SystemEvent systemEvent)
        {
            await _repo.AddAsync(systemEvent);
        }
    }
}
