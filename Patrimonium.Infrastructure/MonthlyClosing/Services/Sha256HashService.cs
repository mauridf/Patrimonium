using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Infrastructure.MonthlyClosing.Services
{
    public class Sha256HashService : IHashService
    {
        public string GenerateHash(object data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
