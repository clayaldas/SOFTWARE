using ClinicApp.Notifications;
using ClinicApp.Storage;

namespace ClinicApp;

class Program
{
    static void Main(string[] args)
    {
        IPatientStore patientStore = new InMemoryPatientStore();
        IDentistStore dentistStore = new InMemoryDentistStore();
        IDentalOfficeStore officeStore = new InMemoryDentalOfficeStore();
        IAppointmentStore appointmentStore = new InMemoryAppointmentStore();

        List<INotificationChannel> channels = new List<INotificationChannel>
        {
            new EmailNotifier(),
            new WhatsAppNotifier(),
        };

        AppointmentScheduler scheduler = new AppointmentScheduler(
            patientStore,
            dentistStore,
            officeStore,
            appointmentStore,
            channels
        );

        Patient patientJuan = new Patient("Juan Perez", "juan@correo.com");
        Dentist dentistCarlos = new Dentist("Carlos Ruiz", "carlos@clinicapp.com");
        DentalOffice centralOffice = new DentalOffice("Consultorio Centro");

        patientStore.Add(patientJuan);
        dentistStore.Add(dentistCarlos);
        officeStore.Add(centralOffice);

        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = start.AddHours(1);

        Appointment appointment = scheduler.Schedule(
            patientJuan.GetId(),
            dentistCarlos.GetId(),
            centralOffice.GetId(),
            start,
            end
        );

        Console.WriteLine("Cita creada: " + appointment.GetId());

        // Buscar todas las citas del paciente Juan
        IEnumerable<Appointment> appointments = appointmentStore.Search(
            null,
            null,
            patientJuan.GetId(),
            null
        );

        Console.WriteLine("\nCitas encontradas:");

        foreach (Appointment item in appointments)
        {
            Console.WriteLine("Id: " + item.GetId());
            Console.WriteLine("Fecha: " + item.GetStart());
        }
    }
}
