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
    options.UseSqlServer("Server=tcp:kishanazureserver.database.windows.net,1433;Initial Catalog=AzureFunction;Persist Security Info=False;User ID=kishanserver;Password=Optimus@12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"));

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add middleware
builder.UseMiddleware<GlobalExceptionMiddleware>();

builder.Build().Run();
