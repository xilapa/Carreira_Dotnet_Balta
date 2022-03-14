namespace PaymentContext.Shared.Entites;

public abstract class Entity
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}