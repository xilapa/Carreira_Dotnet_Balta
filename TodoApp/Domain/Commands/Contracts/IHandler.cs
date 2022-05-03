namespace TodoApp.Domain.Commands.Contracts;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> Handle(T command);
}