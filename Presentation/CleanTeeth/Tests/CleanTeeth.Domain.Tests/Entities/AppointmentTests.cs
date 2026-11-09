using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Tests.Entities;

[TestClass]
public sealed class AppointmentTests
{
    [TestMethod]
    public void Constructor_FutureInterval_CreatesScheduledAppointment()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var dentistId = Guid.NewGuid();
        var dentalOfficeId = Guid.NewGuid();
        var timeInterval = CreateFutureInterval();

        // Act
        var appointment = new Appointment(patientId, dentistId, dentalOfficeId, timeInterval);

        // Assert
        Assert.AreEqual(patientId, appointment.PatientId);
        Assert.AreEqual(dentistId, appointment.DentistId);
        Assert.AreEqual(dentalOfficeId, appointment.DentalOfficeId);
        Assert.AreEqual(timeInterval, appointment.TimeInterval);
        Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status);
        Assert.AreNotEqual(Guid.Empty, appointment.Id);
    }

    [TestMethod]
    public void Constructor_PastInterval_ThrowsBusinessRuleException()
    {
        // Arrange
        var start = DateTimeOffset.UtcNow.AddDays(-1);
        var timeInterval = new TimeInterval(start, start.AddHours(1));
        var patientId = Guid.NewGuid();
        var dentistId = Guid.NewGuid();
        var dentalOfficeId = Guid.NewGuid();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() =>
            new Appointment(patientId, dentistId, dentalOfficeId, timeInterval)
        );
    }

    [TestMethod]
    public void Cancel_ScheduledAppointment_ChangesStatusToCancelled()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();

        // Act
        appointment.Cancel();

        // Assert
        Assert.AreEqual(AppointmentStatus.Cancelled, appointment.Status);
    }

    [TestMethod]
    public void Cancel_CancelledAppointment_ThrowsBusinessRuleException()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();
        appointment.Cancel();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => appointment.Cancel());
    }

    [TestMethod]
    public void Cancel_CompletedAppointment_ThrowsBusinessRuleException()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();
        appointment.Complete();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => appointment.Cancel());
    }

    [TestMethod]
    public void Complete_ScheduledAppointment_ChangesStatusToCompleted()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();

        // Act
        appointment.Complete();

        // Assert
        Assert.AreEqual(AppointmentStatus.Completed, appointment.Status);
    }

    [TestMethod]
    public void Complete_CompletedAppointment_ThrowsBusinessRuleException()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();
        appointment.Complete();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => appointment.Complete());
    }

    [TestMethod]
    public void Complete_CancelledAppointment_ThrowsBusinessRuleException()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();
        appointment.Cancel();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() => appointment.Complete());
    }

    [TestMethod]
    public void Cancel_CompletedAppointment_KeepsStatusCompleted()
    {
        // Arrange
        var appointment = CreateScheduledAppointment();
        appointment.Complete();

        // Act
        Assert.ThrowsExactly<BusinessRuleException>(() => appointment.Cancel());

        // Assert
        Assert.AreEqual(AppointmentStatus.Completed, appointment.Status);
    }

    // Métodos de fábrica
    private static TimeInterval CreateFutureInterval()
    {
        var start = DateTimeOffset.UtcNow.AddDays(1);
        return new TimeInterval(start, start.AddHours(1));
    }

    private static Appointment CreateScheduledAppointment()
    {
        return new Appointment(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateFutureInterval()
        );
    }

    [TestMethod]
    public void Constructor_EcuadorStartThreeHoursAhead_CreatesScheduledAppointment()
    {
        // Arrange: tres horas en el futuro,
        // expresadas en hora de Ecuador (UTC-5)
        var start = DateTimeOffset.UtcNow.AddHours(3).ToOffset(TimeSpan.FromHours(-5));
        var timeInterval = new TimeInterval(start, start.AddHours(1));

        // Act
        var appointment = new Appointment(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            timeInterval
        );

        // Assert
        Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status);
    }

    [TestMethod]
    public void Constructor_EmptyPatientId_ThrowsBusinessRuleException()
    {
        // Arrange
        var dentistId = Guid.NewGuid();
        var dentalOfficeId = Guid.NewGuid();
        var timeInterval = CreateFutureInterval();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() =>
            new Appointment(Guid.Empty, dentistId, dentalOfficeId, timeInterval)
        );
    }

    [TestMethod]
    public void Constructor_EmptyDentistId_ThrowsBusinessRuleException()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var dentalOfficeId = Guid.NewGuid();
        var timeInterval = CreateFutureInterval();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() =>
            new Appointment(patientId, Guid.Empty, dentalOfficeId, timeInterval)
        );
    }

    [TestMethod]
    public void Constructor_EmptyDentalOfficeId_ThrowsBusinessRuleException()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var dentistId = Guid.NewGuid();
        var timeInterval = CreateFutureInterval();

        // Act & Assert
        Assert.ThrowsExactly<BusinessRuleException>(() =>
            new Appointment(patientId, dentistId, Guid.Empty, timeInterval)
        );
    }
}
