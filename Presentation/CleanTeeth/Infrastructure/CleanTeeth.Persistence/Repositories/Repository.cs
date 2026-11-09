using CleanTeeth.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CleanTeethDbContext _context;

    public Repository(CleanTeethDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public Task<T> AddAsync(T entity)
    {
        _context.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        return Task.CompletedTask;
    }
}
