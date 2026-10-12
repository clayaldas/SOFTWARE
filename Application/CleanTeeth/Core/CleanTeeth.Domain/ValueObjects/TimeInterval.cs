using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public record TimeInterval
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public TimeInterval(DateTimeOffset start, DateTimeOffset end)
    {
        if (start >= end)
        {
            throw new BusinessRuleException(
                "La fecha de inicio debe ser anterior a la fecha de fin"
            );
        }

        Start = start;
        End = end;
    }
}
