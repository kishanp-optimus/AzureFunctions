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
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureFunction.Persistance.Migrations;
using Microsoft.AspNetCore.Hosting.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//string keyVaultUrl = "https://azurekishanshared2.vault.azure.net/";

//// Create a SecretClient to access Azure Key Vault
//var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

//// Retrieve the database connection string from Key Vault
//KeyVaultSecret dbConnectionSecret = secretClient.GetSecret("DefaultConnection");
//string dbConnectionString = dbConnectionSecret.Value;
var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection")));
//Server = tcp:kishanazureserver.database.windows.net,1433; Initial Catalog = AzureFunction; Persist Security Info=False; User ID = kishanserver; Password =Optimus@12345; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;
// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add middleware
builder.UseMiddleware<GlobalExceptionMiddleware>();

builder.Build().Run();
