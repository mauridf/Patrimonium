using Patrimonium.Application.DTOs.Consolidation;
using Patrimonium.Application.Interfaces;
using Patrimonium.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Patrimonium.Infrastructure.MonthlyClosing.Providers
{
    public class PropertyDataProvider : IPropertyDataProvider
    {
        private readonly PatrimoniumDbContext _db;

        public PropertyDataProvider(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyCollection<PropertyDto>> GetPropertiesAsync(Guid userId)
        {
            return await _db.Properties
                .Where(p => p.UserId == userId)
                .Select(p => new PropertyDto
                {
                    Id = p.Id,
                    InternalName = p.InternalName,
                    Type = p.Type.ToString(),
                    Purpose = p.Purpose.ToString(),
                    EstimatedValue = p.Valuation.EstimatedValue,
                    InvestedValue = p.Valuation.InvestedValue
                }).ToListAsync();
        }
    }
}
