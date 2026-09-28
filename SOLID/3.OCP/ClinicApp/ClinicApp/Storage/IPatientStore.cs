namespace ClinicApp.Storage;

public interface IPatientStore
{
    void Add(Patient patient);
    Patient? GetById(Guid id);
    IEnumerable<Patient> GetAll();
}
