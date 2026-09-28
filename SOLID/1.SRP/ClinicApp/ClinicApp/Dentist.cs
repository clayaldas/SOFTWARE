namespace ClinicApp;

public class Dentist
{
    public Guid id;
    public string nm;
    public string em;

    public Dentist(string nm, string em)
    {
        ContactValidator.Validate(nm, em);

        this.nm = nm;
        this.em = em;
        this.id = Guid.NewGuid();

        ClinicManager.GetInstance().AllDentists.Add(this);
    }
}
