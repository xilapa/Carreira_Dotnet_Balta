using FluentValidation;

namespace TodoApp.Domain.Commands.MarkTodoAsDone;

public class MarkTodoAsDoneCommandValidator : AbstractValidator<MarkTodoAsDoneCommand>
{
    public MarkTodoAsDoneCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O id do todo não pode ser vazio");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("O id do usuário não pode ser vazio");
    }
}