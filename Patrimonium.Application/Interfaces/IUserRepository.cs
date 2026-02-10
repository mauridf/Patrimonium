using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
