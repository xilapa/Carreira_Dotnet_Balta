using TodoApp.Domain.Commands.Base;

namespace TodoApp.Domain.Commands.UpdateTodo;

public class UpdateTodoCommand : Command<UpdateTodoCommand>
{
    public UpdateTodoCommand(Guid Id, string title, Guid userId, DateTime date) : this()
    {
        Id = Id;
        Title = title;
        UserId = userId;
        Date = date;
    }

    public UpdateTodoCommand() : base(new UpdateTodoCommandValidator())
    { }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}