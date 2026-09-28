using ClinicApp.Storage;

namespace ClinicApp;

public class AppointmentScheduler
{
    private readonly IPatientStore _patients;
    private readonly IDentistStore _dentists;
    private readonly IDentalOfficeStore _offices;
    private readonly IAppointmentStore _appointments;

    public AppointmentScheduler(
        IPatientStore patients,
        IDentistStore dentists,
        IDentalOfficeStore offices,
        IAppointmentStore appointments
    )
    {
        _patients = patients;
        _dentists = dentists;
        _offices = offices;
        _appointments = appointments;
    }

    public Appointment Schedule(
        Guid patientId,
        Guid dentistId,
        Guid officeId,
        DateTime start,
        DateTime end
    )
    {
        Patient? patient = _patients.GetById(patientId);
        Dentist? dentist = _dentists.GetById(dentistId);
        DentalOffice? office = _offices.GetById(officeId);

        if (patient == null)
        {
            throw new KeyNotFoundException("El paciente no existe.");
        }
        if (dentist == null)
        {
            throw new KeyNotFoundException("El dentista no existe.");
        }
        if (office == null)
        {
            throw new KeyNotFoundException("El consultorio no existe.");
        }

        ValidateNoDoubleBooking(dentistId, start, end);


        Appointment appointment = new Appointment(patientId, dentistId, officeId, start, end);
        _appointments.Add(appointment);

        SendMail(
            patient,
            $"Su cita ha sido programada para el {start.ToString("dd/MM/yyyy")} de {start.ToString("HH:mm")} a {end.ToString("HH:mm")} en el: {office.GetName()}."
        );

        return appointment;
    }

    private void ValidateNoDoubleBooking(Guid dentistId, DateTime start, DateTime end)
    {
        // TODO: verificar que el dentista no tenga otra cita en este horario aqui (RF4).
        Console.WriteLine("Validacion de doble-reserva pendiente para el dentista: " + dentistId);
    }

    private void SendMail(Patient patient, string message)
    {
        // TODO: implementar el envio de correo electronico aqui (RF5).
        Console.WriteLine($"Correo enviado a: {patient.GetEmail()} - {message}");
    }
}
