namespace ClinicApp.Notifications;

public class WhatsAppNotifier : INotificationChannel
{
    public void Send(Appointment appointment)
    {
        // TODO: enviaria un mensaje de WhatsApp con los datos de la cita aqui
        Console.WriteLine("WhatsApp enviado para la cita: " + appointment.GetId());
    }
}
