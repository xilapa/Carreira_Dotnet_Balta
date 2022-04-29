namespace TodoApp.Domain.Entities;

public class User : Entity
{
    public User(string googleId, string name)
    {
        GoogleId = googleId;
        Name = name;
        Active = true;
    }

    public string GoogleId { get; private set; }
    public string Name { get; private set; }
    public bool Active { get; private set; }

    public void Deactivate() => Active = false;
}