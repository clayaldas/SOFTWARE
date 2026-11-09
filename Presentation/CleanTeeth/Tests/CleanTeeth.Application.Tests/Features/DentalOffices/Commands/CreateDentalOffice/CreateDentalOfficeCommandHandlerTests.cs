using CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.Tests.TestDoubles;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Application.Tests.Features.DentalOffices.Commands.CreateDentalOffice;

[TestClass]
public sealed class CreateDentalOfficeCommandHandlerTests
{
    public TestContext TestContext { get; set; } = null!;

    [TestMethod]
    public async Task Handle_ValidName_StoresDentalOffice()
    {
        // Arrange
        var session = new InMemorySession();
        var repository = new InMemoryDentalOfficeRepository(session);
        var unitOfWork = new InMemoryUnitOfWork(session);
        var handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        var command = new CreateDentalOfficeCommand { Name = "Consultorio Norte" };

        TestContext.WriteLine("Iniciando prueba de creación exitosa.");

        // Act
        var id = await handler.Handle(command);

        TestContext.WriteLine($"Identificador generado: {id}");

        // Assert
        var stored = await repository.GetByIdAsync(id);
        Assert.IsNotNull(stored);

        TestContext.WriteLine("RESULTADO: El consultorio fue almacenado correctamente.");
    }

    [TestMethod]
    public async Task Handle_EmptyName_ThrowsBusinessRuleException()
    {
        // Arrange
        var session = new InMemorySession();
        var repository = new InMemoryDentalOfficeRepository(session);
        var unitOfWork = new InMemoryUnitOfWork(session);
        var handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        var command = new CreateDentalOfficeCommand { Name = "" };

        TestContext.WriteLine("Iniciando prueba de nombre vacío.");

        // Act & Assert
        var exception = await Assert.ThrowsExactlyAsync<BusinessRuleException>(() =>
            handler.Handle(command)
        );

        TestContext.WriteLine($"Excepción capturada: {exception.GetType().Name}");
        TestContext.WriteLine($"Mensaje: {exception.Message}");

        //Assert.AreEqual(0, session.New.Count);
        Assert.IsEmpty(session.New);

        TestContext.WriteLine(
            "RESULTADO: Se rechazó el nombre vacío y no quedaron entidades nuevas pendientes."
        );
    }

    [TestMethod]
    public async Task Handle_CommitFails_RollsBackAndRethrows()
    {
        // Arrange
        var session = new InMemorySession();
        var repository = new InMemoryDentalOfficeRepository(session);
        var unitOfWork = new FailingUnitOfWork();
        var handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        var command = new CreateDentalOfficeCommand { Name = "Consultorio Norte" };

        TestContext.WriteLine("Iniciando prueba de fallo durante Commit.");

        // Act & Assert
        var exception = await Assert.ThrowsExactlyAsync<InvalidOperationException>(() =>
            handler.Handle(command)
        );

        TestContext.WriteLine($"Excepción capturada: {exception.GetType().Name}");
        TestContext.WriteLine($"Mensaje: {exception.Message}");

        Assert.IsTrue(unitOfWork.RollbackCalled);

        TestContext.WriteLine("RESULTADO: Se propagó la excepción y se solicitó el rollback.");
    }

    [TestMethod]
    public async Task Handle_AddFails_RollsBackAndRethrows()
    {
        // Arrange
        var repository = new FailingDentalOfficeRepository();
        var unitOfWork = new FailingUnitOfWork();
        var handler = new CreateDentalOfficeCommandHandler(repository, unitOfWork);
        var command = new CreateDentalOfficeCommand { Name = "Consultorio Norte" };

        // Act & Assert
        await Assert.ThrowsExactlyAsync<TimeoutException>(
            () => handler.Handle(command));
        Assert.IsFalse(unitOfWork.CommitCalled);
        Assert.IsTrue(unitOfWork.RollbackCalled);
    }

}
