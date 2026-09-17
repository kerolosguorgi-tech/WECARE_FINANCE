using Microsoft.EntityFrameworkCore;
using WECAREFinance.Api;
using WECAREFinance.Application.Abstractions;
using WECAREFinance.Application.Authentication;
using WECAREFinance.Application.Authorization;
using WECAREFinance.Infrastructure;
using WECAREFinance.Infrastructure.Authentication;
using WECAREFinance.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFoundationSecurity();
builder.Services.AddFoundationAuthentication();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReferenceNumberService, ReferenceNumberService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IWorkstationSettingsService, WorkstationSettingsService>();

builder.Services.AddHealthChecks().AddCheck<PostgresHealthCheck>("postgres");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SessionAuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapFoundationAuthentication();

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
    Results.Ok(new { sample = referenceNumberService.Generate("INV") }));

app.MapGet("/api/security/me", (ICurrentUser currentUser) =>
{
    if (!currentUser.IsAuthenticated)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        currentUser.UserId,
        currentUser.UserName,
        currentUser.IsPowerAdmin,
        systemView = currentUser.HasPermission(Permissions.SystemView),
        systemConfigure = currentUser.HasPermission(Permissions.SystemConfigure)
    });
});

app.Run();

public partial class Program;
