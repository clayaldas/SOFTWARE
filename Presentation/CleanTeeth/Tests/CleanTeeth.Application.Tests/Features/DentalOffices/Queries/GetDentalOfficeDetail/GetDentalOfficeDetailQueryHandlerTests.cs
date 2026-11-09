using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Features.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.Tests.TestDoubles;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Tests.Features.DentalOffices.Queries.GetDentalOfficeDetail;

[TestClass]
public sealed class GetDentalOfficeDetailQueryHandlerTests
{
    [TestMethod]
    public async Task Handle_ExistingId_ReturnsDetailDto()
    {
        // Arrange
        var session = new InMemorySession();
        var dentalOffice = new DentalOffice("Consultorio Norte");
        session.Store.Add(dentalOffice);
        var repository = new InMemoryDentalOfficeRepository(session);
        var handler = new GetDentalOfficeDetailQueryHandler(repository);
        var query = new GetDentalOfficeDetailQuery { Id = dentalOffice.Id };

        // Act
        var dto = await handler.Handle(query);

        // Assert
        Assert.AreEqual(dentalOffice.Id, dto.Id);
        Assert.AreEqual("Consultorio Norte", dto.Name);
    }

    [TestMethod]
    public async Task Handle_UnknownId_ThrowsNotFoundException()
    {
        // Arrange
        var session = new InMemorySession();
        var repository = new InMemoryDentalOfficeRepository(session);
        var handler = new GetDentalOfficeDetailQueryHandler(repository);
        var query = new GetDentalOfficeDetailQuery { Id = Guid.NewGuid() };

        // Act & Assert
        await Assert.ThrowsExactlyAsync<NotFoundException>(
            () => handler.Handle(query));
    }
}
