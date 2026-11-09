using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Tests;

public static class TestDatabase
{
    private const string ConnectionString =
        "Server=.\\SQLEXPRESS;Database=CleanTeethTests;Trusted_Connection=True;TrustServerCertificate=True;";

    public static CleanTeethDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<CleanTeethDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        return new CleanTeethDbContext(options);
    }
}
