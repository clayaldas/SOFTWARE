using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Tests.TestDoubles;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Application.Tests.Utilities;

[TestClass]
public sealed class SimpleMediatorTests
{
    [TestMethod]
    public async Task Send_ValidCommand_ReturnsIdOfStoredDentalOffice()
    {
        // Arrange
        var session = new InMemorySession();
        using var provider = BuildServiceProvider(session);
        using var scope = provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new CreateDentalOfficeCommand { Name = "Consultorio Norte" };

        // Act
        var id = await mediator.Send(command);

        // Assert
        var repository = scope.ServiceProvider.GetRequiredService<IDentalOfficeRepository>();
        var stored = await repository.GetByIdAsync(id);
        Assert.IsNotNull(stored);
    }

    [TestMethod]
    public async Task Send_EmptyName_ThrowsCustomValidationException()
    {
        // Arrange
        var session = new InMemorySession();
        using var provider = BuildServiceProvider(session);
        using var scope = provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var command = new CreateDentalOfficeCommand { Name = "" };

        // Act & Assert
        await Assert.ThrowsExactlyAsync<CustomValidationException>(
            () => mediator.Send(command));
    }

    [TestMethod]
    public async Task Send_UnregisteredRequest_ThrowsMediatorException()
    {
        // Arrange
        var session = new InMemorySession();
        using var provider = BuildServiceProvider(session);
        using var scope = provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var request = new UnregisteredRequest();

        // Act & Assert
        await Assert.ThrowsExactlyAsync<MediatorException>(
            () => mediator.Send(request));
    }

    [TestMethod]
    public async Task Send_ExistingIdQuery_ReturnsDetailDto()
    {
        // Arrange
        var session = new InMemorySession();
        var dentalOffice = new DentalOffice("Consultorio Norte");
        session.Store.Add(dentalOffice);
        using var provider = BuildServiceProvider(session);
        using var scope = provider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new GetDentalOfficeDetailQuery { Id = dentalOffice.Id };

        // Act
        var dto = await mediator.Send(query);

        // Assert
        Assert.AreEqual(dentalOffice.Id, dto.Id);
    }

    private static ServiceProvider BuildServiceProvider(InMemorySession session)
    {
        var services = new ServiceCollection();
        services.AddApplicationServices();
        services.AddSingleton(session);
        services.AddScoped<IDentalOfficeRepository, InMemoryDentalOfficeRepository>();
        services.AddScoped<IUnitOfWork, InMemoryUnitOfWork>();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
    }

    private sealed class UnregisteredRequest : IRequest<int>
    {
    }
}
