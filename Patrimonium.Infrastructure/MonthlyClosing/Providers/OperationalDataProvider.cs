using Patrimonium.Application.DTOs.Consolidation;
using Patrimonium.Application.Interfaces;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.MonthlyClosing.Providers
{
    public class OperationalDataProvider : IOperationalDataProvider
    {
        private readonly PatrimoniumDbContext _db;

        public OperationalDataProvider(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task<OperationalDataDto> GetOperationalDataAsync(Guid userId, int year, int month)
        {
            var activeProps = await _db.Properties.CountAsync(p => p.UserId == userId && p.Status == "Active");
            var vacantProps = await _db.Properties.CountAsync(p => p.UserId == userId && p.Status == "Vacant");
            var activeContracts = await _db.Contracts.CountAsync(c => c.UserId == userId && c.Status == "Active");
            var openMaint = await _db.Maintenances.CountAsync(m => m.UserId == userId && m.Status == "Open");

            return new OperationalDataDto
            {
                ActiveProperties = activeProps,
                VacantProperties = vacantProps,
                ActiveContracts = activeContracts,
                OpenMaintenances = openMaint
            };
        }
    }
}
