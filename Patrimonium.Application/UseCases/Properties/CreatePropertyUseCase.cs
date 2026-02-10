using Patrimonium.Application.DTOs.Property;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Services;
using Patrimonium.Domain.Interfaces;

namespace Patrimonium.Application.UseCases.Properties
{
    public class CreatePropertyUseCase : ICreatePropertyUseCase
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IUnitOfWork _uow;
        private readonly PropertyDomainService _domainService;

        public CreatePropertyUseCase(
            IRepository<Property> propertyRepo,
            IUnitOfWork uow,
            PropertyDomainService domainService)
        {
            _propertyRepo = propertyRepo;
            _uow = uow;
            _domainService = domainService;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreatePropertyDto dto)
        {
            var property = new Property
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                InternalName = dto.InternalName,
                Type = dto.Type,
                Purpose = dto.Purpose,
                Status = dto.Status,
                Street = dto.Street,
                Number = dto.Number,
                Neighborhood = dto.Neighborhood,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                BuiltAreaM2 = dto.BuiltAreaM2,
                TotalAreaM2 = dto.TotalAreaM2,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domainService.ValidateCreation(property);

            await _propertyRepo.AddAsync(property);
            await _uow.CommitAsync();

            return property.Id;
        }
    }
}
