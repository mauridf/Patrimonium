using Microsoft.EntityFrameworkCore;
using Patrimonium.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Patrimonium.Application.Interfaces;
using Patrimonium.Infrastructure.Auth;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Infrastructure.Data;
using Patrimonium.Infrastructure.Data.Repositories;
using Patrimonium.Domain.Services;
using Patrimonium.Application.UseCases.Properties;
using Patrimonium.Application.UseCases.Financial;
using Patrimonium.Infrastructure.Data.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PatrimoniumDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PropertyDomainService>();
builder.Services.AddScoped<FinancialDomainService>();
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();

builder.Services.AddScoped<ICreateFinancialTransactionUseCase, CreateFinancialTransactionUseCase>();
builder.Services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
