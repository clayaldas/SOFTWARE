using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.ValueObjects;

public record TimeInterval
{
  public DateTime Start { get; }
  public DateTime End { get; }

  public TimeInterval(DateTime start, DateTime end)
  {
    if (start > end)
    {
      throw new BusinessRuleException(@"La fecha de inicio debe ser anterior a la fecha de fin");
    }

    Start = start;
    End = end;
  }
}
