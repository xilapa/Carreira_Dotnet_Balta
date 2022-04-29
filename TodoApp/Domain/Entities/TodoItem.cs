namespace TodoApp.Domain.Entities;

public class TodoItem : Entity
{
    public TodoItem(string title, DateTime date, Guid userId)
    {
        Title = title;
        Done = false;
        Date = date;
        UserId = userId;
    }

    public string Title { get; private set; }
    public bool Done { get; private set; }
    public DateTime Date { get; private set; }
    public Guid UserId { get; private set; }

    public void MarkAsDone() => Done = true;

    public void MarkAsUndone() => Done = false;

    public void UpdateTitle(string title) => Title = title;
}