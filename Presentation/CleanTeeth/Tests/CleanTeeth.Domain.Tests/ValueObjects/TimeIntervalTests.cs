using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.ValueObjects;

[TestClass]
public sealed class TimeIntervalTests
{
    private static readonly TimeSpan EcuadorOffset = TimeSpan.FromHours(-5);

    [TestMethod]
    public void Constructor_StartBeforeEnd_StoresStartAndEnd()
    {
        // Arrange
        var start = new DateTimeOffset(2030, 1, 15, 10, 0, 0, EcuadorOffset);
        var end = new DateTimeOffset(2030, 1, 15, 11, 0, 0, EcuadorOffset);

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
        var start = new DateTimeOffset(2030, 1, 15, 11, 0, 0, EcuadorOffset);
        var end = new DateTimeOffset(2030, 1, 15, 10, 0, 0, EcuadorOffset);

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => new TimeInterval(start, end));
    }

    [TestMethod]
    public void Constructor_StartEqualsEnd_ThrowsBusinessRuleException()
    {
        // Arrange
        var start = new DateTimeOffset(2030, 1, 15, 10, 0, 0, EcuadorOffset);

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => new TimeInterval(start, start));
    }

    [TestMethod]
    public void Equals_SameStartAndEnd_ReturnsTrue()
    {
        // Arrange
        var start = new DateTimeOffset(2030, 1, 15, 10, 0, 0, EcuadorOffset);
        var end = new DateTimeOffset(2030, 1, 15, 11, 0, 0, EcuadorOffset);
        var first = new TimeInterval(start, end);
        var second = new TimeInterval(start, end);

        // Act
        var areEqual = first.Equals(second);

        // Assert
        Assert.IsTrue(areEqual);
    }
}
