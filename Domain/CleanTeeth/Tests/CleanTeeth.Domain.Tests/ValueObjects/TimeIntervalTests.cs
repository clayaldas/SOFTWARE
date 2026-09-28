using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.ValueObjects;

[TestClass]
public sealed class TimeIntervalTests
{
  [TestMethod]
  public void Constructor_StartBeforeEnd_StoresStartAndEnd()
  {
    // Arrange
    var start = new DateTime(2030, 1, 15, 10, 0, 0);
    var end = new DateTime(2030, 1, 15, 11, 0, 0);

    // Act
    var interval = new TimeInterval(start, end);

    // Assert
    Assert.AreEqual(start, interval.Start);
    Assert.AreEqual(end, interval.End);
  }

  [TestMethod]
  public void Constructor_StartAfterEnd_ThrowsBusinessRuleException()
  {
    // Arrange
    var start = new DateTime(2030, 1, 15, 11, 0, 0);
    var end = new DateTime(2030, 1, 15, 10, 0, 0);

    // Act & Assert
    Assert.ThrowsExactly<BusinessRuleException>(
        () => new TimeInterval(start, end));
  }

  [TestMethod]
  public void Equals_SameStartAndEnd_ReturnsTrue()
  {
    // Arrange
    var start = new DateTime(2030, 1, 15, 10, 0, 0);
    var end = new DateTime(2030, 1, 15, 11, 0, 0);
    var first = new TimeInterval(start, end);
    var second = new TimeInterval(start, end);

    // Act
    var areEqual = first.Equals(second);

    // Assert
    Assert.IsTrue(areEqual);
  }
}
