using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PatrimoniumDbContext _db;

        public UserRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task<bool> EmailExists(string email)
            => await _db.Users.AnyAsync(x => x.Email == email.ToLower());

        public async Task<User?> GetByEmail(string email)
            => await _db.Users.FirstOrDefaultAsync(x => x.Email == email.ToLower());

        public async Task Add(User user)
            => await _db.Users.AddAsync(user);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}
