using CleanTeeth.Application.Contracts.Persistence;

namespace CleanTeeth.Application.Tests.TestDoubles;

public class InMemoryUnitOfWork : IUnitOfWork
{
    private readonly InMemorySession _session;

    public InMemoryUnitOfWork(InMemorySession session)
    {
        _session = session;
    }

    public Task CommitAsync()
    {
        _session.Store.AddRange(_session.New);
        foreach (var office in _session.Removed)
        {
            _session.Store.Remove(office);
        }
        ClearPending();
        return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
        ClearPending(); // descarta sin escribir nada
        return Task.CompletedTask;
    }

    private void ClearPending()
    {
        _session.New.Clear();
        _session.Dirty.Clear();
        _session.Removed.Clear();
    }
}
