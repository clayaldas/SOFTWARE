using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Tests.TestDoubles;

public class FailingDentalOfficeRepository : IDentalOfficeRepository
{
    public Task<DentalOffice> AddAsync(DentalOffice entity)
    {
        return Task.FromException<DentalOffice>(
            new TimeoutException("La base de datos no responde")
        );
    }

    public Task<DentalOffice?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DentalOffice>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DentalOffice entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(DentalOffice entity)
    {
        throw new NotImplementedException();
    }
}
