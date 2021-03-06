namespace Store.Shared.Command;

public interface ICommandHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}