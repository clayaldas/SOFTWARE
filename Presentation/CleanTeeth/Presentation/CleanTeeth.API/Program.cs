using CleanTeeth.API.Middlewares;
using CleanTeeth.Application;
using CleanTeeth.Persistence;

namespace CleanTeeth.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices(builder.Configuration);        

        var app = builder.Build();

        app.UseErrorHandling();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}