using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Patrimonium.Application.DTOs.Auth;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IUnitOfWork uow, IConfiguration config)
        {
            _userRepo = userRepo;
            _uow = uow;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existing = await _userRepo.GetByEmailAsync(dto.Email);
            if (existing != null) throw new Exception("Usuário já existe");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserId = Guid.Empty, // root user
                CreatedBy = "system"
            };

            await _userRepo.AddAsync(user);
            await _uow.CommitAsync();

            return GenerateToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Credenciais inválidas");

            return GenerateToken(user);
        }

        private AuthResponseDto GenerateToken(User user)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(jwt["ExpireHours"]!)),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}
