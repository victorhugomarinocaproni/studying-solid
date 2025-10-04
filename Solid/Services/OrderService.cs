using Solid.Contracts;
using Solid.Entities;
using Solid.Exceptions;
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

    public Order GetOrder(int orderId)
    {
        var order = orderRepository.GetById(orderId);
        if (order == null)
        {
            throw new EntityNotFoundException($"Order {orderId} not found.");
        }
        return order;
    }
}