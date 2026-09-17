using WECAREFinance.Api;
using Microsoft.EntityFrameworkCore;
using WECAREFinance.Application.Abstractions;
using WECAREFinance.Infrastructure;
using WECAREFinance.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFoundationSecurity();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReferenceNumberService, ReferenceNumberService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IWorkstationSettingsService, WorkstationSettingsService>();

builder.Services.AddHealthChecks()
    .AddCheck<PostgresHealthCheck>("postgres");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");

app.MapGet("/api/system/info", () => new
{
    application = "WECARE Finance",
    module = "01 - Foundation & Core Architecture",
    status = "In progress",
    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
    localDeployment = true,
    postgresRequired = true
});

app.MapGet("/api/system/reference-sample", (IReferenceNumberService referenceNumberService) =>
{
    var sample = referenceNumberService.Generate("INV");
    return Results.Ok(new { sample });
});

app.Run();

public partial class Program;
