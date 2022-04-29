namespace TodoApp.Domain.Commands;

public class MarkTodoAsDoneCommand : Command<MarkTodoAsDoneCommand>
{
    public MarkTodoAsDoneCommand() : base(new MarkTodoAsDoneCommandValidator())
    { }

    public MarkTodoAsDoneCommand(Guid id, Guid userId) : this()
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}