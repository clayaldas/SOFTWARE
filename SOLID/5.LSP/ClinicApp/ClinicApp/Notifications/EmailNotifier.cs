
namespace ClinicApp.Notifications;

public class EmailNotifier : INotificationChannel
{
    public void Send(Appointment appointment)
    {
        // TODO: enviaria un correo con los datos de la cita aqui
        Console.WriteLine("Correo enviado para la cita: " + appointment.GetId());
    }
}
