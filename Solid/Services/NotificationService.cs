using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class NotificationService(INotificationSender notificationSender)
{
    public void NotifyUserOrderCreated(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Received);
    }
    
    public void NotifyUserOrderApproved(string customerName)
    {
        notificationSender.NotifyOrderStatus(customerName, OrderStatus.Approved);
    }
}