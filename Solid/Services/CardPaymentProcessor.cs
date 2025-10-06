using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class CardPaymentProcessor(
    OrderService orderService) : IPaymentProcessor
{
    public void Process(Order order, float amount)
    {
        
        if (amount <= order.TotalPrice)
        {
            Console.WriteLine("Insufficient funds");
            return;
        }
        
        Console.WriteLine("Processing Card Payment...");
        // Simulate card validation
        Console.WriteLine("Card Validated!");
        orderService.UpdateOrderStatus(order.Id, OrderStatus.Approved);
    }
}