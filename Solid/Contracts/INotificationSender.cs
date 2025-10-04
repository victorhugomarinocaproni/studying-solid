using Solid.Entities;

namespace Solid.Contracts;

public interface INotificationSender
{
    public void NotifyOrderStatus(string customerName, OrderStatus orderStatus);
}