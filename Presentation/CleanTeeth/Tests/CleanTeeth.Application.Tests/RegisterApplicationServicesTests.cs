using CleanTeeth.Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Application.Tests;

[TestClass]
public sealed class RegisterApplicationServicesTests
{
    [TestMethod]
    public void AddApplicationServices_EveryRequest_HasRegisteredHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddApplicationServices();
        var types = typeof(IMediator).Assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && !type.IsGenericTypeDefinition);
        var missing = new List<string>();

        // Act
        foreach (var type in types)
        {
            foreach (var contract in type.GetInterfaces())
            {
                if (contract.IsGenericType
                    && contract.GetGenericTypeDefinition() == typeof(IRequest<>))
                {
                    var responseType = contract.GetGenericArguments()[0];
                    var handlerType = typeof(IRequestHandler<,>)
                        .MakeGenericType(type, responseType);

                    if (!services.Any(service => service.ServiceType == handlerType))
                    {
                        missing.Add(type.Name);
                    }
                }
            }
        }

        // Assert
        Assert.IsEmpty(missing,
            $"Solicitudes sin Handler registrado: {string.Join(", ", missing)}");
    }
}
