using LoginPageAPIs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Set up the connection string
        var connectionString = "Server=AHMED_REZK\\SQLEXPRESS; Database=LoginPageApi; TrustServerCertificate=True; Trusted_Connection=True;MultipleActiveResultSets=true;"; // This can be from the appsettings.json

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
