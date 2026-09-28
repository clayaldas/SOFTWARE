namespace ClinicApp;

public class ClinicManager
{
    private static ClinicManager _instance;

    public static ClinicManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ClinicManager();
        }
        return _instance;
    }

    public List<Patient> AllPatients = new List<Patient>();
    public List<Dentist> AllDentists = new List<Dentist>();
    public List<DentalOffice> AllOffices = new List<DentalOffice>();
    public List<Appointment> AllAppointments = new List<Appointment>();

    private ClinicManager() { }
}
