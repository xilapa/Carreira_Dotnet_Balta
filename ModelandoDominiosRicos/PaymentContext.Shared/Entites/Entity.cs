using PaymentContext.Shared.Notifications;

namespace PaymentContext.Shared.Entites;

public abstract class Entity : Notifiable
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}