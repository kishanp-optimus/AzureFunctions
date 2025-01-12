using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace AzureFunction.Persistance.Data
{
    //Do I even need this?
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=CPC-kisha-DDRXX;Database=AzureFunction;Trusted_Connection=True;TrustServerCertificate=True");


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
