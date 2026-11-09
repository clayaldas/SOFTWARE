using CleanTeeth.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.API.Tests;

public sealed class ApiFactory : WebApplicationFactory<Program>
{
    private const string DatabaseServer = ".\\SQLEXPRESS";

    private const string ConnectionString =
        "Server=" + DatabaseServer + ";Database=CleanTeethApiTests;" +
        "Trusted_Connection=True;TrustServerCertificate=True;";

    public static CleanTeethDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<CleanTeethDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        return new CleanTeethDbContext(options);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(
            "ConnectionStrings:CleanTeethConnectionString", ConnectionString);
    }
}
