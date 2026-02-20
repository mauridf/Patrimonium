using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.DTOs.Consolidation;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Enums;
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
            var activeProps = await _db.Properties
                .Where(p => p.UserId == userId && p.Status == PropertyStatus.Active)
                .CountAsync();
            var vacantProps = await _db.Properties
                .Where(p => p.UserId == userId && p.Status == PropertyStatus.Vacant)
                .CountAsync();
            var activeContracts = await _db.Contracts
                .Where(c => c.UserId == userId && c.Status == ContractStatus.Active)
                .CountAsync();
            var openMaint = await _db.Maintenances
                .Where(m => m.UserId == userId && m.Status == MaintenanceStatus.Open)
                .CountAsync();

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
