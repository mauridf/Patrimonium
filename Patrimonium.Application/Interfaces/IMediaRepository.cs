using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IMediaRepository
    {
        Task Add(Media media);
        Task<List<Media>> GetByProperty(Guid propertyId);
        Task SaveChanges();
    }
}