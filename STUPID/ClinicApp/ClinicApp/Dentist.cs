namespace ClinicApp;

public class Dentist
{
    public Guid id;
    public string nm;
    public string em;

    public Dentist(string nm, string em)
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

        ClinicManager.GetInstance().AllDentists.Add(this);
    }
}
