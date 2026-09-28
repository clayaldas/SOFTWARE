namespace ClinicApp.Storage;

public class InMemoryDentalOfficeStore : IDentalOfficeStore
{
    private readonly List<DentalOffice> _dentalOffices = new List<DentalOffice>();

    public void Add(DentalOffice dentalOffice)
    {
        _dentalOffices.Add(dentalOffice);
    }

    public DentalOffice? GetById(Guid id)
    {
        foreach (DentalOffice dentalOffice in _dentalOffices)
        {
            if (dentalOffice.GetId() == id)
            {
                return dentalOffice;
            }
        }
        return null;
    }

    public IEnumerable<DentalOffice> GetAll()
    {
        return _dentalOffices;
    }
}
