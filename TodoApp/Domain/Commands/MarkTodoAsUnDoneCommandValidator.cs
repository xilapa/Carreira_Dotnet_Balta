using FluentValidation;

namespace TodoApp.Domain.Commands;

public class MarkTodoAsUnDoneCommandValidator : AbstractValidator<MarkTodoAsUnDoneCommand>
{
    public MarkTodoAsUnDoneCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("O id do todo não pode ser vazio");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("O id do usuário não pode ser vazio");
    }
}