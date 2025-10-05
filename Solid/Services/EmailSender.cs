using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class EmailSender : INotificationSender
{
    public void NotifyOrderStatus(string customerName, OrderStatus orderStatus)
    {
        Console.WriteLine($"Email sent to {customerName}: Order {orderStatus:G}");
    }
    
    
}