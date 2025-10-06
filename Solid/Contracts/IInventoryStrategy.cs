using Solid.Entities;

namespace Solid.Contracts;

public interface IInventoryStrategy
{
    public bool Validate(Inventory inventory, List<OrderItem> orderItems);
}