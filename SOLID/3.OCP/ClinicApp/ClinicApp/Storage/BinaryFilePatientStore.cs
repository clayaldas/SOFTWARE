namespace ClinicApp.Storage;

public class BinaryFilePatientStore : IPatientStore
{
    public void Add(Patient patient)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Patient> GetAll()
    {
        throw new NotImplementedException();
    }

    public Patient? GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
