using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class PixPaymentProcessor(
    OrderService orderService) : IPaymentProcessor, IQrCodeGenerator
{
    public void Process(Order order)
    {
        Console.WriteLine("Generating Pix QrCode...");
        GenerateQrCode();
        Console.WriteLine("Pix Received!");
        orderService.UpdateOrderStatus(order.Id, OrderStatus.Approved);
    }
    public void GenerateQrCode()
    {
        // Just a simulation
    }
}