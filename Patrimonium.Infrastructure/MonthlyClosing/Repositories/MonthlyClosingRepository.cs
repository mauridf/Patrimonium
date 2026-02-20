using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.MonthlyClosing.Repositories
{
    public class MonthlyClosingRepository : IMonthlyClosingRepository
    {
        private readonly PatrimoniumDbContext _db;

        public MonthlyClosingRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task SaveAsync(Patrimonium.Domain.Entities.MonthlyClosing closing)
        {
            _db.MonthClosings.Add(closing);
            await _db.SaveChangesAsync();
        }

        public async Task<Patrimonium.Domain.Entities.MonthlyClosing?> GetByPeriodAsync(Guid userId, int year, int month)
        {
            return await _db.MonthClosings
                .Include(x => x.Snapshot)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.Period.Year == year &&
                    x.Period.Month == month);
        }
    }
}
