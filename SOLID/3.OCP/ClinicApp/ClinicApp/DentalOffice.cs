namespace ClinicApp;

public class DentalOffice
{
    private readonly Guid _id;
    private readonly string _name;

    public DentalOffice(string name)
    {
        if (name == null || name == "")
        {
            throw new ArgumentException("El nombre no puede estar vacio.");
        }

        _id = Guid.NewGuid();
        _name = name;
    }

    public Guid GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }
}
