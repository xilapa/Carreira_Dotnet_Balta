namespace TodoApp.Domain.Commands.Base;

public abstract class BaseHandler
{
    private readonly List<string> _errors = new ();
    public IReadOnlyCollection<string> Errors => _errors.ToArray();

    public void AddErrors(params string[] errors) => _errors.AddRange(errors);
    public bool IsValid() => _errors.Count == 0;
}