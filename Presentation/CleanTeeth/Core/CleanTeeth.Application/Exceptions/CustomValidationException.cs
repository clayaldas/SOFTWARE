namespace CleanTeeth.Application.Exceptions;

public class CustomValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base(BuildMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildMessage(IReadOnlyDictionary<string, string[]> errors)
    {
        var messages = errors.Values.SelectMany(fieldMessages => fieldMessages);
        return "La solicitud no es válida: " + string.Join(" ", messages);
    }
}
