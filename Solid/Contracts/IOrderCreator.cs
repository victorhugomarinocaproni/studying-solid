using Solid.Entities;

namespace Solid.Contracts;

public interface IOrderCreator
{
    public Order CreateNewOrder(
        string customerName,
        List<OrderItem> orderItems,
        CustomerCategory customerCategory);
}