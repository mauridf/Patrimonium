using Patrimonium.Application.DTOs.Documents;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class DocumentService
    {
        private readonly IDocumentRepository _repo;
        private readonly IFileStorageService _storage;

        public DocumentService(IDocumentRepository repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task Upload(
            Guid userId,
            CreateDocumentDto dto,
            Stream fileStream,
            string fileName,
            string contentType
        )
        {
            var url = await _storage.UploadAsync(fileStream, fileName, contentType);

            var doc = new Document(
                userId,
                dto.PropertyId,
                dto.ContractId,
                dto.Type,
                url,
                dto.Description
            );

            await _repo.Add(doc);
            await _repo.SaveChanges();
        }
    }
}