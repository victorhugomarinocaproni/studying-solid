using Solid.Contracts;

namespace Solid.Services;

public class CustomerSpendingCalculator(IOrderRepository orderRepository) : ICustomerSpendingCalculator
{
    public float CalculateTotalSpending(string customerName)
    {
        var orders = orderRepository.GetOrdersByCustomerName(customerName);
        return orders.Sum(order => order.TotalPrice);
    }
}