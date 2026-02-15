using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task Add(Person person);
        Task<List<Person>> GetByUser(Guid userId);
        Task<Person?> GetById(Guid id, Guid userId);
        Task SaveChanges();
    }
}
