using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Tests.TestDoubles;

public class InMemorySession
{
    public List<DentalOffice> Store { get; } = new(); // lo confirmado
    public List<DentalOffice> New { get; } = new(); // nuevos
    public List<DentalOffice> Dirty { get; } = new(); // modificados
    public List<DentalOffice> Removed { get; } = new(); // eliminados
}
