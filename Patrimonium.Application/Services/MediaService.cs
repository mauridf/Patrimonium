using Patrimonium.Application.DTOs.Media;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class MediaService
    {
        private readonly IMediaRepository _repo;
        private readonly IFileStorageService _storage;

        public MediaService(IMediaRepository repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task Upload(
            CreateMediaDto dto,
            Stream fileStream,
            string fileName,
            string contentType
        )
        {
            var url = await _storage.UploadAsync(fileStream, fileName, contentType);

            var media = new Media(
                dto.PropertyId,
                dto.Type,
                url,
                dto.IsCover
            );

            await _repo.Add(media);
            await _repo.SaveChanges();
        }
    }
}