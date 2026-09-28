namespace ClinicApp.Storage;

public class InMemoryPatientStore : IPatientStore
{
    private readonly List<Patient> _patients = new List<Patient>();

    public void Add(Patient patient)
    {
        _patients.Add(patient);
    }

    public Patient? GetById(Guid id)
    {
        foreach (Patient patient in _patients)
        {
            if (patient.GetId() == id)
            {
                return patient;
            }
        }
        return null;
    }

    public IEnumerable<Patient> GetAll()
    {
        return _patients;
    }
}
