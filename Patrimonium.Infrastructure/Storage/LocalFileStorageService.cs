using Patrimonium.Application.Interfaces;

namespace Patrimonium.Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath = "storage";

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            Directory.CreateDirectory(_basePath);

            var filePath = Path.Combine(_basePath, $"{Guid.NewGuid()}_{fileName}");

            using var fs = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(fs);

            return filePath.Replace("\\", "/");
        }
    }
}