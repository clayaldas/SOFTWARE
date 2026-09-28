namespace ClinicApp;

public class Patient
{
    public Guid id;
    public string nm;
    public string em;

    public Patient(string nm, string em)
    {
        ContactValidator.Validate(nm, em);

        this.nm = nm;
        this.em = em;
        this.id = Guid.NewGuid();

        ClinicManager.GetInstance().AllPatients.Add(this);
    }
}
