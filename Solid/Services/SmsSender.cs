using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class SmsSender : INotificationSender
{
    public void NotifyOrderStatus(string customerName, OrderStatus orderStatus)
    {
        Console.WriteLine($"SMS sent to {customerName}: Order {orderStatus:G}");
    }
}