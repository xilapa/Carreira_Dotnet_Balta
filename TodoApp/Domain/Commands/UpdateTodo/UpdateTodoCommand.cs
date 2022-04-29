using TodoApp.Domain.Commands.Base;

namespace TodoApp.Domain.Commands.UpdateTodo;

public class UpdateTodoCommand : Command<UpdateTodoCommand>
{
    public UpdateTodoCommand(string title, Guid userId, DateTime date) : this()
    {
        Title = title;
        UserId = userId;
        Date = date;
    }

    public UpdateTodoCommand() : base(new UpdateTodoCommandValidator())
    { }

    public string Title { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}