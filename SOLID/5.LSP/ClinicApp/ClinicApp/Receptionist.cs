using ClinicApp.Scheduling;

namespace ClinicApp;

public class Receptionist : IScheduler
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _email;
    private readonly AppointmentScheduler _scheduler;

    public Receptionist(string name, string email, AppointmentScheduler scheduler)
    {
        ContactValidator.Validate(name, email);
        _id = Guid.NewGuid();
        _name = name;
        _email = email;
        _scheduler = scheduler;
    }

    public Guid GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetEmail()
    {
        return _email;
    }

    public Appointment Schedule(
        Guid patientId,
        Guid dentistId,
        Guid officeId,
        DateTime start,
        DateTime end
    )
    {
        return _scheduler.Schedule(patientId, dentistId, officeId, start, end);
    }

    public void Cancel(Appointment appointment)
    {
        appointment.Cancel();
    }
}
