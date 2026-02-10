using Patrimonium.Application.DTOs.Person;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;

namespace Patrimonium.Application.UseCases.Person
{
    public class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IRepository<Domain.Entities.Person> _repo;
        private readonly IUnitOfWork _uow;
        private readonly PersonDomainService _domain;

        public CreatePersonUseCase(
            IRepository<Domain.Entities.Person> repo,
            IUnitOfWork uow,
            PersonDomainService domain)
        {
            _repo = repo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreatePersonDto dto)
        {
            var p = new Domain.Entities.Person
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = dto.Name,
                Type = dto.Type,
                CpfCnpj = dto.CpfCnpj,
                Email = dto.Email,
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domain.Validate(p);

            await _repo.AddAsync(p);
            await _uow.CommitAsync();

            return p.Id;
        }
    }
}
