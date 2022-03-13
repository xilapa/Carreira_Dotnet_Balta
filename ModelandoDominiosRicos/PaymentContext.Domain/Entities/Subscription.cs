namespace PaymentContext.Domain.Entities;

public class Subscription
{
    public Subscription(DateTime? expireDate)
    {
        Active = true;
        CreateDate = DateTime.UtcNow;
        LastUpdateDate = DateTime.UtcNow;
        ExpireDate = expireDate;
        _payments = new List<Payment>();
    }

    public bool Active { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public DateTime? ExpireDate { get; private set; }

    private readonly List<Payment> _payments;
    public IReadOnlyCollection<Payment> Payments { get => _payments.ToArray(); }

    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }

    public void Inactivate()
    {
        Active = false;
        LastUpdateDate = DateTime.UtcNow;
    }
}