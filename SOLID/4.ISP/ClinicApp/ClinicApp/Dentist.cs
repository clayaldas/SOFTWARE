using ClinicApp.Scheduling;

namespace ClinicApp;

public class Dentist : IScheduler, ITreatmentProvider
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _email;

    // Nueva variable agregada para permitir agendamiento de citas
    // inyectada a través del constructor
    private readonly AppointmentScheduler _scheduler;

    public Dentist(string name, string email, AppointmentScheduler scheduler)
    {
        ContactValidator.Validate(name, email);

        _id = Guid.NewGuid();
        _name = name;
        _email = email;
       
        // Esto permite que el dentista pueda agendar citas a través del scheduler
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

    public void CompleteTreatment(Appointment appointment)
    {
        appointment.Complete();
    }
}
