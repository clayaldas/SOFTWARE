namespace ClinicApp;

public class DentalOffice
{
    public Guid id;
    public string nm;

    public DentalOffice(string nm)
    {
        if (nm == null || nm == "")
        {
            throw new Exception("error");
        }
        this.nm = nm;
        this.id = Guid.NewGuid();
        ClinicManager.GetInstance().AllOffices.Add(this);
    }
}
