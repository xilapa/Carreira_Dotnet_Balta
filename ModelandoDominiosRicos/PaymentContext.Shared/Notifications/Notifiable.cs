namespace PaymentContext.Shared.Notifications;

public class Notifiable
{
    public List<Notification> Notifications { get; set; }

    public void AddNotification(string property, string message)
    {
        var notification = new Notification(property, message);
        Notifications.Add(notification);
    }
}