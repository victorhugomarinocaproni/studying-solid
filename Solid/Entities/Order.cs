namespace Solid.Entities;
public class Order
{
    public string CustomerName { get; }
    public List<OrderItem> Items { get; }
    public float TotalPrice { get; set; }
    public OrderStatus Status { get; }
    public DateTime OrderDate { get; }
    public CustomerCategory CustomerCategory { get; }

    public Order(
        string customerName,
        List<OrderItem> items,
        float totalPrice,
        OrderStatus status,
        DateTime orderDate,
        CustomerCategory customerCategory)
    {
        CustomerName = customerName;
        Items = items;
        TotalPrice = totalPrice;
        Status = status;
        OrderDate = orderDate;
        CustomerCategory = customerCategory;
    }
    
    public Order(
        int id,
        string customerName,
        List<OrderItem> items,
        float totalPrice,
        OrderStatus status,
        DateTime orderDate,
        CustomerCategory customerCategory)
        : this(customerName, items, totalPrice, status, orderDate, customerCategory)
    {
    }
}