namespace TodoApp.Domain.Commands.Contracts;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}