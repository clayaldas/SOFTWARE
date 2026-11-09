using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Tests.Entities;

[TestClass]
public sealed class DentalOfficeTests
{
  [TestMethod]
  public void Constructor_ValidName_StoresNameAndGeneratesId()
  {
    // Arrange
    var name = "Consultorio Norte";

    // Act
    var dentalOffice = new DentalOffice(name);

    // Assert
    Assert.AreEqual(name, dentalOffice.Name);
    Assert.AreNotEqual(Guid.Empty, dentalOffice.Id);
  }

  [TestMethod]
  public void Constructor_NullName_ThrowsBusinessRuleException()
  {
    // Arrange
    string name = null!;

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new DentalOffice(name));
  }

  [TestMethod]
  [DataRow("")]
  [DataRow("   ")]
  public void Constructor_EmptyOrWhitespaceName_ThrowsBusinessRuleException(
      string name)
  {
    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new DentalOffice(name));
  }
}
