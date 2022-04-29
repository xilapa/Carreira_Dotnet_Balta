using FluentValidation;
using TodoApp.Domain.Commands.Contracts;

namespace TodoApp.Domain.Commands;

public class CreateTodoCommand : Command<CreateTodoCommand>
{
    public CreateTodoCommand() : base(new CreateTodoCommandValidator())
    { }

    public CreateTodoCommand(string title, Guid userId, DateTime date) : this()
    {
        Title = title;
        UserId = userId;
        Date = date;
    }

    public string Title { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}