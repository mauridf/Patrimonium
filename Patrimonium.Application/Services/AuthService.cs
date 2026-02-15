using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.DTOs.Auth;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Auth;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Application.Services
{
    public class AuthService
    {
        private readonly PatrimoniumDbContext _db;
        private readonly JwtService _jwt;

        public AuthService(PatrimoniumDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(x => x.Email == dto.Email.ToLower()))
                throw new Exception("Email já registrado");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User(dto.Name, dto.Email, hash, dto.Phone);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return _jwt.GenerateToken(user);
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Credenciais inválidas");

            if (!user.IsActive)
                throw new Exception("Usuário inativo");

            return _jwt.GenerateToken(user);
        }
    }
}
