namespace ClinicApp.Storage;

public class InMemoryAppointmentStore : IAppointmentStore
{
    private readonly List<Appointment> _appointments = new List<Appointment>();

    public void Add(Appointment appointment)
    {
        _appointments.Add(appointment);
    }

    public Appointment? GetById(Guid id)
    {
        foreach (Appointment appointment in _appointments)
        {
            if (appointment.GetId() == id)
            {
                return appointment;
            }
        }
        return null;
    }

    public IEnumerable<Appointment> GetAll()
    {
        return _appointments;
    }

    // Implementa el método Search según la interfaz IAppointmentStore
    public IEnumerable<Appointment> Search(
        DateTime? date,
        Guid? officeId,
        Guid? patientId,
        Guid? dentistId
    )
    {
        IEnumerable<Appointment> result = _appointments;

        if (date.HasValue)
        {
            result = result.Where(a => a.GetStart().Date == date.Value.Date);
        }
        if (officeId.HasValue)
        {
            result = result.Where(a => a.GetOfficeId() == officeId.Value);
        }
        if (patientId.HasValue)
        {
            result = result.Where(a => a.GetPatientId() == patientId.Value);
        }
        if (dentistId.HasValue)
        {
            result = result.Where(a => a.GetDentistId() == dentistId.Value);
        }

        return result;
    }
}
