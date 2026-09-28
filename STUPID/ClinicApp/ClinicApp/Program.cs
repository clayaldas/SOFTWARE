namespace ClinicApp;

internal class Program
{
    static void Main(string[] args)
    {
        Patient patient = new Patient("John Doe", "johndoe@email.com");
        Dentist dentist = new Dentist("Dr. Smith", "dentist@gmail.com");
        DentalOffice dentalOffice = new DentalOffice("Consultorio de limpieza dental");

        Appointment appointment = new Appointment(
            patient.id,
            dentist.id,
            dentalOffice.id,
            DateTime.UtcNow.AddHours(1),
            DateTime.UtcNow.AddHours(2)
        );

        Console.WriteLine("Cita guardada. Id: " + appointment.id);
        Console.WriteLine("Estado inicial (1=pendiente): " + appointment.st);
        Console.WriteLine("Enviando correo a: " + patient.em);

        if (appointment.st == 1)
        {
            appointment.st = 2;
            appointment.flag1 = false;
            Console.WriteLine("Cita cancelada. Estado: " + appointment.st);
        }

        Appointment appointment2 = new Appointment(
            patient.id,
            dentist.id,
            dentalOffice.id,
            DateTime.UtcNow.AddHours(3),
            DateTime.UtcNow.AddHours(4)
        );

        appointment2.DoIt2();
        Console.WriteLine("Despues de DoIt2() — estado: " + appointment2.st);

        Console.WriteLine("Total de pacientes: " + ClinicManager.GetInstance().AllPatients.Count);
        Console.WriteLine("Total de citas: " + ClinicManager.GetInstance().AllAppointments.Count);

        Console.ReadLine();
    }
}
