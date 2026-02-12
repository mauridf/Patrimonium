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

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

#region CONFIG LOAD (ENV → APPSETTINGS → ERROR)

// ConnectionString
var envConn = Environment.GetEnvironmentVariable("DB_CONNECTION");
var appConn = config.GetConnectionString("DefaultConnection");

var connStr = !string.IsNullOrWhiteSpace(envConn)
    ? envConn
    : appConn;

if (string.IsNullOrWhiteSpace(connStr) || connStr.StartsWith("${"))
    throw new Exception("ConnectionString não configurada. Defina DB_CONNECTION ou appsettings.");

// JWT
var jwtKeyEnv = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtSection = config.GetSection("Jwt");

var jwtKey = !string.IsNullOrWhiteSpace(jwtKeyEnv)
    ? jwtKeyEnv
    : jwtSection["Key"];

if (string.IsNullOrWhiteSpace(jwtKey) || jwtKey.StartsWith("${"))
    throw new Exception("JWT_KEY não configurado.");

#endregion

// ===== INTERCEPTOR =====
builder.Services.AddScoped<AuditInterceptor>();

// ===== DB CONTEXT =====
builder.Services.AddDbContext<PatrimoniumDbContext>((sp, options) =>
{
    options.UseNpgsql(connStr);
    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});

// ===== INFRA =====
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();

// ===== AUTH =====
builder.Services.AddScoped<IAuthService, AuthService>();

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

// ===== QUERIES =====
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();
builder.Services.AddScoped<ContractQueryService>();

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

builder.Services.AddScoped<IContractLifecycleUseCase, ContractLifecycleUseCase>();
builder.Services.AddScoped<IProjectionEngine, ProjectionEngine>();
builder.Services.AddScoped<IValuationEngine, ValuationEngine>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IVacancyEngine, VacancyEngine>();

// ===== SERILOG =====
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// ===== JWT =====
var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ===== RATELIMITER =====
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("default", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(
            config.GetValue<int>("RateLimit:WindowSeconds", 10)
        );
        opt.PermitLimit = config.GetValue<int>("RateLimit:PermitLimit", 100);
    });
});

// ===== HEALTH =====
builder.Services.AddHealthChecks();

// ===== CORS (frontend ready) =====
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("frontend", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true); // depois troca por domínio real
    });
});

// ===== API =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== PIPELINE =====
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRateLimiter();
app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
