using CleanTeeth.Application.Contracts.Persistence;

namespace CleanTeeth.Application.Tests.TestDoubles;

public class FailingUnitOfWork : IUnitOfWork
{
    public bool CommitCalled { get; private set; }
    public bool RollbackCalled { get; private set; }

    public Task CommitAsync()
    {
        CommitCalled = true;
        return Task.FromException(new InvalidOperationException("La base de datos no responde"));
    }

    public Task RollbackAsync()
    {
        RollbackCalled = true;
        return Task.CompletedTask;
    }
}
