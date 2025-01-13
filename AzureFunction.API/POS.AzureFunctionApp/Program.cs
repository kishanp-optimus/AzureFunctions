using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
//using POS.AzureFunctionApp.Data;
//using POS.AzureFunctionApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AzureFunction.Persistance.Data;
using AzureFunction.Application.Interface;
using AzureFunction.Persistance.Repository;
using POS.AzureFunctionApp;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=CPC-kisha-DDRXX;Database=AzureFunction;Trusted_Connection=True;TrustServerCertificate=True"));

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add middleware
builder.UseMiddleware<GlobalExceptionMiddleware>();

builder.Build().Run();
