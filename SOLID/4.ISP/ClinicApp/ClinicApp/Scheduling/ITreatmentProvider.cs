namespace ClinicApp.Scheduling;

public interface ITreatmentProvider
{
    void CompleteTreatment(Appointment appointment);
}
