using Patrimonium.Application.DTOs.Media;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;

namespace Patrimonium.Application.UseCases.Media
{
    public class CreateMediaUseCase : ICreateMediaUseCase
    {
        private readonly IRepository<Domain.Entities.Media> _repo;
        private readonly IUnitOfWork _uow;
        private readonly MediaDomainService _domain;

        public CreateMediaUseCase(
            IRepository<Domain.Entities.Media> repo,
            IUnitOfWork uow,
            MediaDomainService domain)
        {
            _repo = repo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateMediaDto dto)
        {
            var media = new Domain.Entities.Media
            {
                Id = Guid.NewGuid(),
                PropertyId = dto.PropertyId,
                Type = dto.Type,
                Url = dto.Url,
                IsCover = dto.IsCover,
                CreatedAt = DateTime.UtcNow
            };

            _domain.Validate(media);

            await _repo.AddAsync(media);
            await _uow.CommitAsync();

            return media.Id;
        }
    }
}
