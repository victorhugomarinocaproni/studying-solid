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
    public int CreateOrder(string customerName, List<OrderItem> orderItems, CustomerCategory customerCategory)
    {
        var order = orderCreator.CreateNewOrder(
            customerName,
            orderItems,
            customerCategory);

        var orderId = orderRepository.Add(order);
        
        notificationService.NotifyUser(customerName, OrderStatus.Approved);
        
        return orderId;
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

    public void UpdateOrderStatus(int orderId, OrderStatus orderStatus)
    {
        var order = GetOrder(orderId);
        order.Status = orderStatus;
        orderRepository.Update(order);
        
        notificationService.NotifyUser(order.CustomerName, order.Status);
    }
}