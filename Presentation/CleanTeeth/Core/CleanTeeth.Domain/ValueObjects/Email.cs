using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new BusinessRuleException("El correo electrónico es requerido");
        }

        if (!email.Contains("@"))
        {
            throw new BusinessRuleException("El correo electrónico no es válido");
        }

        Value = email;
    }
}
