using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IAlertService
    {
        Task CreateAlertAsync(Alert alert);
    }
}
