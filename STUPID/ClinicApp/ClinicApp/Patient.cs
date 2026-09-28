namespace ClinicApp;

public class Patient
{
    public Guid id;
    public string nm;
    public string em;

    public Patient(string nm, string em)
    {
        if (nm == null || nm == "")
        {
            throw new Exception("error");
        }
        if (em == null || em == "")
        {
            throw new Exception("error");
        }
        if (!em.Contains("@"))
        {
            throw new Exception("error");
        }

        this.nm = nm;
        this.em = em;
        this.id = Guid.NewGuid();

        ClinicManager.GetInstance().AllPatients.Add(this);
    }
}
