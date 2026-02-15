using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PatrimoniumDbContext _db;

        public PersonRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Person person)
            => await _db.People.AddAsync(person);

        public async Task<List<Person>> GetByUser(Guid userId)
            => await _db.People
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task<Person?> GetById(Guid id, Guid userId)
            => await _db.People
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}
