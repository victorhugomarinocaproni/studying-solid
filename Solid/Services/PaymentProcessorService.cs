using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class PaymentProcessorService(
    IPaymentProcessor paymentProcessor)
{
    public void ProcessPayment(Order order)
    {
        paymentProcessor.Process(order);
    }
}