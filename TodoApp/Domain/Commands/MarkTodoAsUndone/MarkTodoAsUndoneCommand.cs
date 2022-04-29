using TodoApp.Domain.Commands.Base;

namespace TodoApp.Domain.Commands.MarkTodoAsUndone;

public class MarkTodoAsUndoneCommand : Command<MarkTodoAsUndoneCommand>
{
    public MarkTodoAsUndoneCommand() : base(new MarkTodoAsUndoneCommandValidator())
    { }

    public MarkTodoAsUndoneCommand(Guid id, Guid userId) : this()
    {
        Id = id;
        UserId = userId;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}