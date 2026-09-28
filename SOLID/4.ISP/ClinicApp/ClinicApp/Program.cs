using ClinicApp.Notifications;
using ClinicApp.Storage;

namespace ClinicApp;

class Program
{
    static void Main(string[] args)
    {
        // Crear los almacenes
        IPatientStore patientStore = new InMemoryPatientStore();
        IDentistStore dentistStore = new InMemoryDentistStore();
        IDentalOfficeStore officeStore = new InMemoryDentalOfficeStore();
        IAppointmentStore appointmentStore = new InMemoryAppointmentStore();

        // Crear los canales de notificación
        List<INotificationChannel> channels = new List<INotificationChannel>
        {
            new EmailNotifier(),
            new WhatsAppNotifier(),
        };

        // Crear el servicio que contiene la lógica
        // para programar las citas
        AppointmentScheduler scheduler = new AppointmentScheduler(
            patientStore,
            dentistStore,
            officeStore,
            appointmentStore,
            channels
        );

        // Crear los objetos
        Patient patientJuan = new Patient("Juan Perez", "juan@correo.com");        
        Dentist dentistCarlos = new Dentist(
            "Carlos Ruiz",
            "carlos@clinicapp.com",
            scheduler
        );
        DentalOffice centralOffice = new DentalOffice("Consultorio Centro");

        // Almacenar los objetos
        patientStore.Add(patientJuan);
        dentistStore.Add(dentistCarlos);
        officeStore.Add(centralOffice);

        // Definir fecha y hora de la cita
        DateTime start = DateTime.Now.AddDays(1);
        DateTime end = start.AddHours(1);


        // El dentista programa la cita
        Appointment appointment = dentistCarlos.Schedule(
            patientJuan.GetId(),
            dentistCarlos.GetId(),
            centralOffice.GetId(),
            start,
            end
        );
        Console.WriteLine("Cita creada, por el dentista con el código de cita: " + appointment.GetId());

        // El dentista completa la cita
        dentistCarlos.CompleteTreatment(appointment);        
        Console.WriteLine("Estado tras completar la cita por el dentista: " + appointment.GetStatus());        

        // Buscar todas las citas del paciente Juan
        IEnumerable<Appointment> appointments = appointmentStore.Search(
            null,
            null,
            patientJuan.GetId(),
            null
        );

        Console.WriteLine("\nCitas encontradas para el paciente:");

        foreach (Appointment item in appointments)
        {
            Console.WriteLine("Id: " + item.GetId());
            Console.WriteLine("Fecha: " + item.GetStart());
        }
    }
}
