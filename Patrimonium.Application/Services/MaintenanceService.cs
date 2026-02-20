using Patrimonium.Application.DTOs.Maintenance;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class MaintenanceService
    {
        private readonly IMaintenanceRepository _repo;

        public MaintenanceService(IMaintenanceRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(Guid userId, CreateMaintenanceDto dto)
        {
            var maintenance = new Maintenance(
                userId,
                dto.PropertyId,
                dto.Title,
                dto.Description,
                dto.Priority,
                dto.CostEstimation,
                dto.ResponsiblePersonId
            );

            await _repo.Add(maintenance);
            await _repo.SaveChanges();
        }

        public Task<List<Maintenance>> GetMy(Guid userId)
            => _repo.GetByUser(userId);

        public async Task Start(Guid id, Guid userId)
        {
            var m = await _repo.GetById(id, userId)
                ?? throw new Exception("Maintenance not found");

            m.Start();
            await _repo.SaveChanges();
        }

        public async Task Complete(Guid id, Guid userId, decimal realCost)
        {
            var m = await _repo.GetById(id, userId)
                ?? throw new Exception("Maintenance not found");

            m.Complete(realCost);
            await _repo.SaveChanges();
        }

        public async Task Cancel(Guid id, Guid userId)
        {
            var m = await _repo.GetById(id, userId)
                ?? throw new Exception("Maintenance not found");

            m.Cancel();
            await _repo.SaveChanges();
        }
    }
}