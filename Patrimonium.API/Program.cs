using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Patrimonium.API.Middlewares;
using Patrimonium.Application.Interfaces;
using Patrimonium.Application.Services;
using Patrimonium.Application.UseCases.Automation;
using Patrimonium.Application.UseCases.Contract;
using Patrimonium.Application.UseCases.Dashboard;
using Patrimonium.Application.UseCases.Document;
using Patrimonium.Application.UseCases.Financial;
using Patrimonium.Application.UseCases.Inspections;
using Patrimonium.Application.UseCases.Intelligence;
using Patrimonium.Application.UseCases.Maintenance;
using Patrimonium.Application.UseCases.Media;
using Patrimonium.Application.UseCases.Person;
using Patrimonium.Application.UseCases.Properties;
using Patrimonium.Application.UseCases.Simulation;
using Patrimonium.Domain.Interfaces;
using Patrimonium.Domain.Services;
using Patrimonium.Infrastructure.Auth;
using Patrimonium.Infrastructure.Data;
using Patrimonium.Infrastructure.Data.Context;
using Patrimonium.Infrastructure.Data.Queries;
using Patrimonium.Infrastructure.Data.Repositories;
using Patrimonium.Infrastructure.Persistence.Interceptors;
using Serilog;

Console.WriteLine("==== INICIO Program.cs ====");

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

Console.WriteLine("1 - Builder criado");

// ===== VARIÁVEIS DE AMBIENTE / CONFIG =====
var connStr = config.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connStr))
{
    Console.WriteLine("❌ ERRO CRÍTICO: DefaultConnection não encontrada (env/config).");
    throw new Exception("ConnectionString DefaultConnection não configurada.");
}

var jwtSection = config.GetSection("Jwt");
if (!jwtSection.Exists())
{
    Console.WriteLine("❌ ERRO CRÍTICO: Seção Jwt não encontrada.");
    throw new Exception("Config Jwt ausente.");
}

Console.WriteLine("2 - Config valida (ConnectionString + JWT)");

// ===== INTERCEPTOR =====
builder.Services.AddScoped<AuditInterceptor>();
Console.WriteLine("3 - AuditInterceptor registrado");

// ===== DB CONTEXT =====
builder.Services.AddDbContext<PatrimoniumDbContext>((sp, options) =>
{
    Console.WriteLine(">>> Configurando DbContext");

    options.UseNpgsql(connStr);

    // ⚠️ PONTO PERIGOSO
    // Se AuditInterceptor resolver serviços, I/O, async bloqueado, etc → trava
    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});

Console.WriteLine("4 - DbContext configurado");

// ===== INFRA =====
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
Console.WriteLine("5 - Infra OK");

// ===== AUTH =====
builder.Services.AddScoped<IAuthService, AuthService>();
Console.WriteLine("6 - AuthService OK");

// ===== DOMAIN SERVICES =====
builder.Services.AddScoped<PropertyDomainService>();
builder.Services.AddScoped<FinancialDomainService>();
builder.Services.AddScoped<MaintenanceDomainService>();
builder.Services.AddScoped<PersonDomainService>();
builder.Services.AddScoped<InspectionDomainService>();
builder.Services.AddScoped<DocumentDomainService>();
builder.Services.AddScoped<MediaDomainService>();
builder.Services.AddScoped<ContractDomainService>();
builder.Services.AddScoped<FinancialEngineService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IGovernanceService, GovernanceService>();
Console.WriteLine("7 - DomainServices OK");

// ===== QUERIES =====
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();
Console.WriteLine("8 - Queries OK");

// ===== USECASES =====
builder.Services.AddScoped<AutomationUseCase>();
builder.Services.AddScoped<IIntelligenceUseCase, IntelligenceUseCase>();
builder.Services.AddScoped<ISimulationUseCase, SimulationUseCase>();
builder.Services.AddScoped<IDashboardUseCase, DashboardUseCase>();

builder.Services.AddScoped<ICreateContractUseCase, CreateContractUseCase>();
builder.Services.AddScoped<ICreateMediaUseCase, CreateMediaUseCase>();
builder.Services.AddScoped<ICreateDocumentUseCase, CreateDocumentUseCase>();
builder.Services.AddScoped<ICreateInspectionUseCase, CreateInspectionUseCase>();
builder.Services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
builder.Services.AddScoped<ICreateMaintenanceUseCase, CreateMaintenanceUseCase>();
builder.Services.AddScoped<ICreateFinancialTransactionUseCase, CreateFinancialTransactionUseCase>();
builder.Services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();

Console.WriteLine("9 - UseCases OK");

// ===== SERILOG =====
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

Console.WriteLine("10 - Serilog OK");

// ===== JWT =====
var jwt = jwtSection;
var keyStr = jwt["Key"];

if (string.IsNullOrWhiteSpace(keyStr))
{
    Console.WriteLine("❌ ERRO CRÍTICO: Jwt:Key não definido.");
    throw new Exception("Jwt:Key ausente.");
}

var key = Encoding.UTF8.GetBytes(keyStr);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
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

Console.WriteLine("11 - JWT OK");

// ===== RATELIMITER =====
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("default", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 100;
    });
});

Console.WriteLine("12 - RateLimiter OK");

// ===== HEALTH =====
builder.Services.AddHealthChecks();
Console.WriteLine("13 - HealthChecks OK");

// ===== API =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("14 - API services OK");

// ===== BUILD =====
Console.WriteLine("15 - Antes do Build()");
var app = builder.Build();
Console.WriteLine("16 - Depois do Build()");

// ===== PIPELINE =====
app.UseMiddleware<RequestLoggingMiddleware>();
Console.WriteLine("17 - Middleware OK");

app.UseRateLimiter();
app.MapHealthChecks("/health");
Console.WriteLine("18 - RateLimiter + Health OK");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Console.WriteLine("19 - Swagger OK");
}

app.UseAuthentication();
app.UseAuthorization();
Console.WriteLine("20 - Auth pipeline OK");

app.MapControllers();
Console.WriteLine("21 - Controllers OK");

Console.WriteLine("22 - Antes do app.Run()");
app.Run();
