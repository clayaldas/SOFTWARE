using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Persistence.Repositories;
using CleanTeeth.Persistence.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Persistence;

public static class RegisterPersistenceServices
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CleanTeethConnectionString");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "No existe la cadena de conexión 'CleanTeethConnectionString'.");
        }

        services.AddDbContext<CleanTeethDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDentalOfficeRepository, DentalOfficeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();

        return services;
    }
}
