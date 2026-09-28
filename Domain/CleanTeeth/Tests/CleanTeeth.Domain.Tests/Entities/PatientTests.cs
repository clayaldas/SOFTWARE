using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.Entities;

[TestClass]
public sealed class PatientTests
{
  [TestMethod]
  public void Constructor_ValidData_StoresDataAndGeneratesId()
  {
    // Arrange
    var name = "Luis Mora";
    var email = new Email("luis.mora@correo.com");

    // Act
    var patient = new Patient(name, email);

    // Assert
    Assert.AreEqual(name, patient.Name);
    Assert.AreEqual(email, patient.Email);
    Assert.AreNotEqual(Guid.Empty, patient.Id);
  }

  [TestMethod]
  public void Constructor_NullName_ThrowsBusinessRuleException()
  {
    // Arrange
    string name = null!;
    var email = new Email("luis.mora@correo.com");

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Patient(name, email));
  }

  [TestMethod]
  [DataRow("")]
  [DataRow("   ")]
  public void Constructor_EmptyOrWhitespaceName_ThrowsBusinessRuleException(
      string name)
  {
    // Arrange
    var email = new Email("luis.mora@correo.com");

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Patient(name, email));
  }

  [TestMethod]
  public void Constructor_NullEmail_ThrowsBusinessRuleException()
  {
    // Arrange
    var name = "Luis Mora";
    Email email = null!;

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Patient(name, email));
  }
}
