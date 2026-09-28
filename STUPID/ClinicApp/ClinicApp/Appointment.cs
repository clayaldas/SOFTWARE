namespace ClinicApp;

public class Appointment
{
    public Guid id;
    public Guid pid;
    public Guid did;
    public Guid oid;
    public int st;
    public DateTime dt1;
    public DateTime dt2;
    public bool flag1;

    public Appointment(Guid pid, Guid did, Guid oid, DateTime dt1, DateTime dt2)
    {
        if (dt1 > dt2)
        {
            throw new Exception("error");
        }
        if (dt1 < DateTime.Now)
        {
            throw new Exception("error");
        }

        Patient p = null;
        foreach (Patient x in ClinicManager.GetInstance().AllPatients)
        {
            if (x.id == pid)
            {
                p = x;
                break;
            }
        }
        Dentist d = null;
        foreach (Dentist x in ClinicManager.GetInstance().AllDentists)
        {
            if (x.id == did)
            {
                d = x;
                break;
            }
        }
        if (p == null || d == null)
        {
            throw new Exception("error");
        }

        this.pid = pid;
        this.did = did;
        this.oid = oid;
        this.dt1 = dt1;
        this.dt2 = dt2;
        this.st = 1;
        this.flag1 = true;
        this.id = Guid.NewGuid();

        ClinicManager.GetInstance().AllAppointments.Add(this);
    }

    public void DoIt()
    {
        if (st != 1)
        {
            throw new Exception("error");
        }
        st = 2;
        flag1 = false;
    }

    public void DoIt2()
    {
        if (st != 1)
        {
            throw new Exception("error");
        }
        st = 3;
        flag1 = false;
    }
}
