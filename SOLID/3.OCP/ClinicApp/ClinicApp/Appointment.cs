namespace ClinicApp;

public class Appointment
{
    private readonly Guid _id;
    private readonly Guid _patientId;
    private readonly Guid _dentistId;
    private readonly Guid _officeId;
    private readonly DateTime _start;
    private readonly DateTime _end;
    private AppointmentStatus _status;

    public Appointment(Guid patientId, Guid dentistId, Guid officeId, DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new ArgumentException("La fecha de inicio no puede ser posterior a la de fin.");
        }
        if (start < DateTime.Now)
        {
            throw new ArgumentException("No se puede agendar una cita en el pasado.");
        }

        _id = Guid.NewGuid();
        _patientId = patientId;
        _dentistId = dentistId;
        _officeId = officeId;
        _start = start;
        _end = end;

        _status = AppointmentStatus.Pending;
    }

    public Guid GetId()
    {
        return _id;
    }

    public Guid GetPatientId()
    {
        return _patientId;
    }

    public Guid GetDentistId()
    {
        return _dentistId;
    }

    public Guid GetOfficeId()
    {
        return _officeId;
    }

    public DateTime GetStart()
    {
        return _start;
    }

    public DateTime GetEnd()
    {
        return _end;
    }

    public AppointmentStatus GetStatus()
    {
        return _status;
    }

    public void Cancel()
    {
        // TODO: validar la transición de estado (no cancelar una cita ya completada, etc.)
        // Se implementa cuando corresponda, más adelante en la serie.

        _status = AppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        // TODO: validar la transición de estado (no completar una cita ya cancelada, etc.)
        // Se implementa cuando corresponda, más adelante en la serie.

        _status = AppointmentStatus.Completed;
    }
}
