using Solid.Contracts;
using Solid.Entities;
using Solid.Repositories;

namespace Solid.Services;

public class OrderService(
    OrderRepository orderRepository,
    NotificationService notificationService,
    IOrderCreator orderCreator)
{

    public void CreateOrder(string customerName, List<OrderItem> orderItems, CustomerCategory customerCategory)
    {
        var order = orderCreator.CreateNewOrder(
            customerName,
            orderItems,
            customerCategory);

        orderRepository.Add(order);
        
        notificationService.NotifyUserOrderCreated(customerName);
    }
}