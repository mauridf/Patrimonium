using Patrimonium.Application.DTOs.Property;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _repo;

        public PropertyService(IPropertyRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(Guid userId, CreatePropertyDto dto)
        {
            var property = new Property(
                userId,
                dto.InternalName,
                dto.Type,
                dto.Purpose,
                dto.Status,
                dto.Street,
                dto.Number,
                dto.Complement,
                dto.Neighborhood,
                dto.City,
                dto.State,
                dto.Country,
                dto.ZipCode,
                dto.BuiltAreaM2,
                dto.TotalAreaM2,
                dto.Bedrooms,
                dto.Bathrooms,
                dto.Suites,
                dto.ParkingSpaces,
                dto.Furnished,
                dto.Description
            );

            await _repo.Add(property);
            await _repo.SaveChanges();
        }

        public Task<List<Property>> GetMyProperties(Guid userId)
            => _repo.GetByUser(userId);
    }
}
