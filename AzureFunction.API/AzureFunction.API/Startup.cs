using AzureFunction.Application.Interface;
using AzureFunction.Persistance.Data;
using AzureFunction.Persistance.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
[assembly: FunctionsStartup(typeof(POS.AzureFunctions.Startup))]

namespace POS.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            
            
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=CPC-kisha-DDRXX;Database=AzureFunction;Trusted_Connection=True;TrustServerCertificate=True"));

            // Add repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}