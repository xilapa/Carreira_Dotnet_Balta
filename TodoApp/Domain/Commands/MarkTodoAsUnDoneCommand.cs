namespace TodoApp.Domain.Commands;

public class MarkTodoAsUnDoneCommand : Command<MarkTodoAsUnDoneCommand>
{
    public MarkTodoAsUnDoneCommand() : base(new MarkTodoAsUnDoneCommandValidator())
    { }

    public MarkTodoAsUnDoneCommand(Guid id, Guid userId) : this()
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}