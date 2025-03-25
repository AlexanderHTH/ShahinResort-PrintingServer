using FluentValidation.Results;

namespace PrintingServer.Domain.Exceptions;
public class FluentValidationException(ValidationResult validationResult) : Exception("Validation failed.")
{
    public List<ValidationFailure> Errors { get; } = validationResult.Errors;

    public Dictionary<string, string[]> ToDictionary()
    {
        return Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );
    }
}