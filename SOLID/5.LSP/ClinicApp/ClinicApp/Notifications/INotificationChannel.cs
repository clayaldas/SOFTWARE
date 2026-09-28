namespace ClinicApp.Notifications;

public interface INotificationChannel
{
    void Send(Appointment appointment);
}
