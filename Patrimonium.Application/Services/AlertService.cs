using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.Services
{
    public class AlertService : IAlertService
    {
        private readonly IRepository<Alert> _repo;

        public AlertService(IRepository<Alert> repo)
        {
            _repo = repo;
        }

        public async Task CreateAlertAsync(Alert alert)
        {
            await _repo.AddAsync(alert);
        }
    }
}
