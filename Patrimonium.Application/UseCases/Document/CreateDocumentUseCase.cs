using Patrimonium.Application.DTOs.Document;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;

namespace Patrimonium.Application.UseCases.Document
{
    public class CreateDocumentUseCase : ICreateDocumentUseCase
    {
        private readonly IRepository<Domain.Entities.Document> _repo;
        private readonly IUnitOfWork _uow;
        private readonly DocumentDomainService _domain;

        public CreateDocumentUseCase(
            IRepository<Domain.Entities.Document> repo,
            IUnitOfWork uow,
            DocumentDomainService domain)
        {
            _repo = repo;
            _uow = uow;
            _domain = domain;
        }

        public async Task<Guid> ExecuteAsync(Guid userId, CreateDocumentDto dto)
        {
            var doc = new Domain.Entities.Document
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                PropertyId = dto.PropertyId,
                ContractId = dto.ContractId,
                Type = dto.Type,
                FileUrl = dto.FileUrl,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString()
            };

            _domain.Validate(doc);

            await _repo.AddAsync(doc);
            await _uow.CommitAsync();

            return doc.Id;
        }
    }
}
