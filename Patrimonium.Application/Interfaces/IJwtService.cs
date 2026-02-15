using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
