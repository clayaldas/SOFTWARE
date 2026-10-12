using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.ValueObjects;

[TestClass]
public sealed class EmailTests
{
  [TestMethod]
  public void Constructor_ValidValue_StoresValue()
  {
    // Arrange
    var value = "paciente@cleanteeth.com";

    // Act
    var email = new Email(value);

    // Assert
    Assert.AreEqual(value, email.Value);
  }

  [TestMethod]
  public void Constructor_NullValue_ThrowsBusinessRuleException()
  {
    // Arrange
    string value = null!;

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Email(value));
  }

  [TestMethod]
  [DataRow("")]
  [DataRow("   ")]
  public void Constructor_EmptyOrWhitespaceValue_ThrowsBusinessRuleException(
      string value)
  {
    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Email(value));
  }

  [TestMethod]
  public void Constructor_ValueWithoutAt_ThrowsBusinessRuleException()
  {
    // Arrange
    var value = "paciente.cleanteeth.com";

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new Email(value));
  }

  [TestMethod]
  public void Equals_SameValue_ReturnsTrue()
  {
    // Arrange
    var first = new Email("paciente@cleanteeth.com");
    var second = new Email("paciente@cleanteeth.com");

    // Act
    var areEqual = first.Equals(second);

    // Assert
    Assert.IsTrue(areEqual);
  }

  [TestMethod]
  public void Equals_DifferentValue_ReturnsFalse()
  {
    // Arrange
    var first = new Email("paciente@cleanteeth.com");
    var second = new Email("dentista@cleanteeth.com");

    // Act
    var areEqual = first.Equals(second);

    // Assert
    Assert.IsFalse(areEqual);
  }
}
