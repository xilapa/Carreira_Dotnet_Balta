using Store.Domain.StoreContext.Enums;

namespace Store.Domain.StoreContext.Entities;

public class Order
{
    public Order(Customer customer)
    {
        Customer = customer;
        Number = Guid.NewGuid().ToString().Replace("-", "");
        CreateDate = DateTime.UtcNow;
        Status = EOrderStatus.Created;
        _items= new List<OrderItem>();
        _deliveries = new List<Delivery>();
    }

    public Customer Customer { get; private set; }
    public string Number { get; private set; }
    public DateTime CreateDate { get; private set; }
    public EOrderStatus Status { get; private set; }

    private readonly IList<OrderItem> _items;
    public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
    
    private readonly IList<Delivery> _deliveries;
    public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();

    public void AddItem(OrderItem item)
    {
        //Valida item
        _items.Add(item);
    }
    
    public void Place()
    {
        
    }
}