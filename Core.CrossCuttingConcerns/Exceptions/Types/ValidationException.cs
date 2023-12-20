using System.Runtime.Serialization;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; set; }

    public ValidationException() : base()
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    public ValidationException(string message) : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(string message, IEnumerable<ValidationExceptionModel> errors) : base(message)
    {
        Errors = errors;
    }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        var arr = errors.Select(x =>
                $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}")
            .ToList();

        return $"One or more validation errors occurred: {string.Join(string.Empty, values: arr)}";
    }
}

public class ValidationExceptionModel
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
