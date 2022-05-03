using FluentValidation;
using TodoApp.Domain.Commands.Contracts;

namespace TodoApp.Domain.Commands.Base;

public abstract class Command<T> : ICommand where T : class
{
    private readonly AbstractValidator<T> _validator;
    private readonly List<string> _errors;

    protected Command(AbstractValidator<T> validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _errors = new List<string>();
    }

    public IReadOnlyCollection<string> Errors => _errors.ToArray();

    public bool Valid => _errors.Count == 0;
    public bool InValid => !Valid;
    public bool IsInvalid() => !Validate();
    public bool IsValid() => Validate();

    public bool Validate()
    {
        var validationResult = _validator.Validate((this as T)!);
        _errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
        return validationResult.IsValid;
    }
}