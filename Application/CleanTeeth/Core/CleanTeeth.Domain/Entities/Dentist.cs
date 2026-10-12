using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Dentist
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }

    public Dentist(string name, Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException("El nombre es requerido");
        }

        if (email is null)
        {
            throw new BusinessRuleException("El correo electrónico es requerido");
        }

        Name = name;
        Email = email;
        Id = Guid.CreateVersion7();
    }
}
