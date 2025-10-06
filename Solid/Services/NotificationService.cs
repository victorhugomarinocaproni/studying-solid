using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class NotificationService(INotificationSender notificationSender)
{
    private void NotifyUserOrderCreated(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Received);
    }

    private void NotifyUserOrderApproved(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Approved);
    }

    private void NotifyUserOrderShipped(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Shipped);
    }
    
    private void NotifyUserOrderDelivered(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Delivered);
    }

    public void NotifyUser(string customerName, OrderStatus orderStatus)
    {
        switch (orderStatus)
        {
            case OrderStatus.Pending:
                NotifyUserOrderCreated(customerName);
                break;
            case OrderStatus.Approved:
                NotifyUserOrderApproved(customerName);
                break;
            case OrderStatus.Shipped:
                NotifyUserOrderShipped(customerName);
                break;
            case OrderStatus.Delivered:
                NotifyUserOrderDelivered(customerName);
                break;
        }
    }
}
