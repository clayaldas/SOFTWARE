namespace ClinicApp.Scheduling;

public interface IScheduler
{
    Appointment Schedule(
        Guid patientId,
        Guid dentistId,
        Guid officeId,
        DateTime start,
        DateTime end
    );
    void Cancel(Appointment appointment);
}
