namespace TodoApp.Domain.Commands.Contracts;

public interface ICommand
{
    public IReadOnlyCollection<string> Errors { get; }
    bool Validate();
}