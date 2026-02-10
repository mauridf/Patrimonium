using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.UseCases.Contract;
using Patrimonium.Application.UseCases.Document;
using Patrimonium.Application.UseCases.Financial;
using Patrimonium.Application.UseCases.Inspection;
using Patrimonium.Application.UseCases.Maintenance;
using Patrimonium.Application.UseCases.Media;
using Patrimonium.Application.UseCases.Person;
using Patrimonium.Application.UseCases.Properties;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;
using Patrimonium.Infrastructure.Auth;
using Patrimonium.Infrastructure.Data;
using Patrimonium.Infrastructure.Data.Context;
using Patrimonium.Infrastructure.Data.Queries;
using Patrimonium.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PatrimoniumDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PropertyDomainService>();
builder.Services.AddScoped<FinancialDomainService>();
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();
builder.Services.AddScoped<MaintenanceDomainService>();
builder.Services.AddScoped<PersonDomainService>();
builder.Services.AddScoped<InspectionDomainService>();
builder.Services.AddScoped<DocumentDomainService>();
builder.Services.AddScoped<MediaDomainService>();
builder.Services.AddScoped<ContractDomainService>();
builder.Services.AddScoped<ICreateContractUseCase, CreateContractUseCase>();
builder.Services.AddScoped<ICreateMediaUseCase, CreateMediaUseCase>();
builder.Services.AddScoped<ICreateDocumentUseCase, CreateDocumentUseCase>();
builder.Services.AddScoped<ICreateInspectionUseCase, CreateInspectionUseCase>();
builder.Services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
builder.Services.AddScoped<ICreateMaintenanceUseCase,CreateMaintenanceUseCase>();
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
