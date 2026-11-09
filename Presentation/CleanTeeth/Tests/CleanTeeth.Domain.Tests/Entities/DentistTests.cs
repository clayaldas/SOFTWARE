using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.Entities;

[TestClass]
public sealed class DentistTests
{
  [TestMethod]
  public void Constructor_ValidData_StoresDataAndGeneratesId()
  {
    // Arrange
    var name = "Ana Torres";
    var email = new Email("ana.torres@cleanteeth.com");

    // Act
    var dentist = new Dentist(name, email);

    // Assert
    Assert.AreEqual(name, dentist.Name);
    Assert.AreEqual(email, dentist.Email);
    Assert.AreNotEqual(Guid.Empty, dentist.Id);
  }

  [TestMethod]
  public void Constructor_NullName_ThrowsBusinessRuleException()
  {
    // Arrange
    string name = null!;
    var email = new Email("ana.torres@cleanteeth.com");

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Dentist(name, email));
  }

  [TestMethod]
  [DataRow("")]
  [DataRow("   ")]
  public void Constructor_EmptyOrWhitespaceName_ThrowsBusinessRuleException(
      string name)
  {
    // Arrange
    var email = new Email("ana.torres@cleanteeth.com");

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Dentist(name, email));
  }

  [TestMethod]
  public void Constructor_NullEmail_ThrowsBusinessRuleException()
  {
    // Arrange
    var name = "Ana Torres";
    Email email = null!;

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Dentist(name, email));
  }
}
