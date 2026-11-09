using CleanTeeth.Domain.Entities;
using CleanTeeth.Persistence.Repositories;
using CleanTeeth.Persistence.UnitsOfWork;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Tests.Repositories;

[TestClass]
public sealed class DentalOfficeRepositoryTests
{
    [ClassInitialize]
    public static async Task ClassInitialize(TestContext testContext)
    {
        using var context = TestDatabase.CreateContext();
        await context.Database.EnsureDeletedAsync(testContext.CancellationToken);
        await context.Database.EnsureCreatedAsync(testContext.CancellationToken);
    }

    [TestMethod]
    public async Task CommitAsync_AfterAddAsync_SavesDentalOffice()
    {
        // Arrange
        using var writeContext = TestDatabase.CreateContext();
        var repository = new DentalOfficeRepository(writeContext);
        var unitOfWork = new UnitOfWorkEFCore(writeContext);
        var dentalOffice = new DentalOffice("Consultorio Norte");

        // Act
        await repository.AddAsync(dentalOffice);
        await unitOfWork.CommitAsync();

        // Assert
        using var readContext = TestDatabase.CreateContext();
        var readRepository = new DentalOfficeRepository(readContext);
        var stored = await readRepository.GetByIdAsync(dentalOffice.Id);
        Assert.IsNotNull(stored);
        Assert.AreEqual("Consultorio Norte", stored.Name);
    }

    [TestMethod]
    public async Task AddAsync_WithoutCommit_DoesNotSaveDentalOffice()
    {
        // Arrange
        using var writeContext = TestDatabase.CreateContext();
        var repository = new DentalOfficeRepository(writeContext);
        var dentalOffice = new DentalOffice("Consultorio Sur");

        // Act
        await repository.AddAsync(dentalOffice);

        // Assert
        using var readContext = TestDatabase.CreateContext();
        var readRepository = new DentalOfficeRepository(readContext);
        var stored = await readRepository.GetByIdAsync(dentalOffice.Id);
        Assert.IsNull(stored);
    }

    [TestMethod]
    public async Task CommitAsync_NameLongerThan150_ThrowsDbUpdateException()
    {
        // Arrange
        using var writeContext = TestDatabase.CreateContext();
        var repository = new DentalOfficeRepository(writeContext);
        var unitOfWork = new UnitOfWorkEFCore(writeContext);
        var dentalOffice = new DentalOffice(new string('A', 151));
        await repository.AddAsync(dentalOffice);

        // Act & Assert
        await Assert.ThrowsExactlyAsync<DbUpdateException>(
            () => unitOfWork.CommitAsync());
    }
}
