using ClinicApp.Notifications;
using ClinicApp.Scheduling;
using ClinicApp.Storage;

namespace ClinicApp;

class Program
{
    static void Main(string[] args)
    {
        // Almacenes
        IPatientStore patientStore = new InMemoryPatientStore();
        IDentistStore dentistStore = new InMemoryDentistStore();
        IDentalOfficeStore officeStore = new InMemoryDentalOfficeStore();
        IAppointmentStore appointmentStore = new InMemoryAppointmentStore();

        // Canales de notificación
        List<INotificationChannel> channels = new List<INotificationChannel>
        {
            new EmailNotifier(),
            new WhatsAppNotifier()
        };

        // Servicio que contiene la lógica de programación de citas
        AppointmentScheduler appointmentScheduler = new AppointmentScheduler(
            patientStore,
            dentistStore,
            officeStore,
            appointmentStore,
            channels
        );

        // Entidades
        Patient patientJuan = new Patient(
            "Juan Perez",
            "juan@correo.com"
        );

        Dentist dentistCarlos = new Dentist(
            "Carlos Ruiz",
            "carlos@clinicapp.com",
            appointmentScheduler
        );

        Receptionist receptionistMaria = new Receptionist(
            "Maria Lopez",
            "maria@clinicapp.com",
            appointmentScheduler
        );

        DentalOffice centralOffice = new DentalOffice(
            "Consultorio Centro"
        );

        // Guardar entidades necesarias
        patientStore.Add(patientJuan);
        dentistStore.Add(dentistCarlos);
        officeStore.Add(centralOffice);


        // ==========================================
        // LSP: usamos la abstracción IScheduler
        // ==========================================

        IScheduler scheduler;

        // El Dentist puede ser utilizado como IScheduler
        scheduler = dentistCarlos;

        Appointment appointment1 = scheduler.Schedule(
            patientJuan.GetId(),
            dentistCarlos.GetId(),
            centralOffice.GetId(),
            DateTime.Now.AddDays(1),
            DateTime.Now.AddDays(1).AddHours(1)
        );

        Console.WriteLine(
            "Cita agendada por Dentist: " + appointment1.GetId()
        );


        // Sustituimos Dentist por Receptionist
        scheduler = receptionistMaria;

        Appointment appointment2 = scheduler.Schedule(
            patientJuan.GetId(),
            dentistCarlos.GetId(),
            centralOffice.GetId(),
            DateTime.Now.AddDays(2),
            DateTime.Now.AddDays(2).AddHours(1)
        );

        Console.WriteLine(
            "Cita agendada por Receptionist: " + appointment2.GetId()
        );
    }
}