using Solid.Entities;

namespace Solid.Contracts;

public interface IInventoryStrategy
{
    public void Validate(Inventory inventory, List<OrderItem> orderItems);
}