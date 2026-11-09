using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence;

public class CleanTeethDbContext : DbContext
{
    public CleanTeethDbContext(DbContextOptions<CleanTeethDbContext> options)
        : base(options)
    {
    }

    public DbSet<DentalOffice> DentalOffices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanTeethDbContext).Assembly);
    }
}
