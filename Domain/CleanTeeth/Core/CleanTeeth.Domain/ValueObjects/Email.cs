using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public record Email
{
  public string Value { get; } = null!;

  public Email(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
    {
      throw new BusinessRuleException($"El {nameof(email)} es requerido");
    }

    if (!email.Contains("@"))
    {
      throw new BusinessRuleException($"El {nameof(email)} no es válido");
    }

    Value = email;
  }
}
