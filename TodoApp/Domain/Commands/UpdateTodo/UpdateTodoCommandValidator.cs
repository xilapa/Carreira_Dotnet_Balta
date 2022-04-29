using FluentValidation;

namespace TodoApp.Domain.Commands.UpdateTodo;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MinimumLength(3).WithMessage("O título deve ter no mínimo 3 caracteres");

        RuleFor(c => c.Date)
            .NotEmpty().WithMessage("A data é obrigatória");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("O usuário é obrigatório");
    }
}