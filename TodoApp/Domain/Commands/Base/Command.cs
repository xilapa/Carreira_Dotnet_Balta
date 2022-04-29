using FluentValidation;
using TodoApp.Domain.Commands.Contracts;

namespace TodoApp.Domain.Commands.Base;

public abstract class Command<T> : ICommand where T : class
{
    private readonly AbstractValidator<T> _validator;
    private List<string> _errors { get; set; }

    protected Command(AbstractValidator<T> validator)
    {
        if(validator is null)
            throw new ArgumentNullException(nameof(validator));

        _validator = validator;
        _errors = new List<string>();
    }

    public IReadOnlyCollection<string> Errors => _errors.ToArray();

    public bool Validate()
    {
        var validationResult = _validator.Validate((this as T)!);
        _errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
        return validationResult.IsValid;
    }
}