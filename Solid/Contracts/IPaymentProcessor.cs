using Solid.Entities;

namespace Solid.Contracts;

public interface IPaymentProcessor
{
    public void Process(Order order, float amount);
}