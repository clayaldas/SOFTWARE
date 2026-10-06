using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class DentalOffice
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public DentalOffice(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException("El nombre del consultorio es requerido");
        }

        Name = name;
        Id = Guid.CreateVersion7();
    }
}
