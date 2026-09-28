namespace ClinicApp.Storage;

public interface IDentistStore
{
    void Add(Dentist dentist);
    Dentist? GetById(Guid id);
    IEnumerable<Dentist> GetAll();
}
