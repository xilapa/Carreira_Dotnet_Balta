using FluentValidation;
using TodoApp.Domain.Commands.Contracts;

namespace TodoApp.Domain.Commands;

public class CreateTodoCommand : ICommand
{
    private readonly AbstractValidator<CreateTodoCommand> _validator;
    private List<string> _errors { get; set; }

    public CreateTodoCommand() { }

    public CreateTodoCommand(string title, Guid userId, DateTime date)
    {
        Title = title;
        UserId = userId;
        Date = date;
        _errors = new List<string>();
        _validator = new CreateTodoCommandValidator();
    }

    public string Title { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }

    public IReadOnlyCollection<string> Errors => _errors.ToArray();

    public bool Validate()
    {
        var validationResult = _validator.Validate(this);
        _errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
        return validationResult.IsValid;
    }
}