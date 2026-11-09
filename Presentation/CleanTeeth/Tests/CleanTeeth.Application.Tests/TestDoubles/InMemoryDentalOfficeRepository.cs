using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Tests.TestDoubles;

public class InMemoryDentalOfficeRepository : IDentalOfficeRepository
{
    private readonly InMemorySession _session;

    public InMemoryDentalOfficeRepository(InMemorySession session)
    {
        _session = session;
    }

    public Task<DentalOffice?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_session.Store.FirstOrDefault(o => o.Id == id));
    }

    public Task<IEnumerable<DentalOffice>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<DentalOffice>>(_session.Store.ToList());
    }

    public Task<DentalOffice> AddAsync(DentalOffice entity)
    {
        _session.New.Add(entity); // registra; todavía no guarda
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(DentalOffice entity)
    {
        _session.Dirty.Add(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(DentalOffice entity)
    {
        _session.Removed.Add(entity);
        return Task.CompletedTask;
    }
}
