namespace ClinicApp;

public class Dentist
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _email;

    public Dentist(string name, string email)
    {
        ContactValidator.Validate(name, email);

        _id = Guid.NewGuid();
        _name = name;
        _email = email;
    }

    public Guid GetId()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetEmail()
    {
        return _email;
    }
}
