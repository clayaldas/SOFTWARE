namespace ClinicApp.Storage;

public class InMemoryDentistStore : IDentistStore
{
    private readonly List<Dentist> _dentists = new List<Dentist>();

    public void Add(Dentist dentist)
    {
        _dentists.Add(dentist);
    }

    public Dentist? GetById(Guid id)
    {
        foreach (Dentist dentist in _dentists)
        {
            if (dentist.GetId() == id)
            {
                return dentist;
            }
        }
        return null;
    }

    public IEnumerable<Dentist> GetAll()
    {
        return _dentists;
    }
}

