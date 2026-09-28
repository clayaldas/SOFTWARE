namespace ClinicApp.Storage;

public interface IAppointmentStore
{
    void Add(Appointment appointment);
    Appointment? GetById(Guid id);
    IEnumerable<Appointment> GetAll();
    IEnumerable<Appointment> Search(
        DateTime? date,
        Guid? officeId,
        Guid? patientId,
        Guid? dentistId
    );
}

