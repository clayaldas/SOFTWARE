namespace ClinicApp.Storage;

public interface IDentalOfficeStore
{
    void Add(DentalOffice dentalOffice);
    DentalOffice? GetById(Guid id);
    IEnumerable<DentalOffice> GetAll();
}
