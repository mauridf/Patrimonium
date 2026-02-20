using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.Services;
using Patrimonium.Infrastructure.Auth;
using Patrimonium.Infrastructure.Data.Context;
using Patrimonium.Infrastructure.Repositories;
using Patrimonium.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

#region ==============================
#region Configuração de Banco de Dados
#endregion ==============================

builder.Services.AddDbContext<PatrimoniumDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

#endregion

#region ==============================
#region Configuração JWT / Auth
#endregion ==============================

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });

#endregion

#region ==============================
#region Injeção de Dependência (DI)
#endregion ==============================

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<FinancialTransactionService>();
builder.Services.AddScoped<MaintenanceService>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<MediaService>();
builder.Services.AddScoped<InspectionService>();

// Interfaces
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();

#endregion

#region ==============================
#region Controllers, Swagger e API
#endregion ==============================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region ==============================
#region Pipeline HTTP
#endregion ==============================

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
