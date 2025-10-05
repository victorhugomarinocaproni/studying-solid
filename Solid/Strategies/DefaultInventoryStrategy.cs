using Solid.Contracts;
using Solid.Entities;
using Solid.Exceptions;

namespace Solid.Strategies;

public class DefaultInventoryStrategy : IInventoryStrategy
{
    public void Validate(Inventory inventory, List<OrderItem> orderItems)
    {
        foreach (var item in orderItems)
        {
            var product = inventory.InventoryItems
                .FirstOrDefault(ii => ii.ProductName == item.ProductName);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product {item.ProductName} not found");
            }

            if (product.Quantity < item.Quantity)
            {
                throw new InsufficientStockException($"Product {item.ProductName} is out of stock");
            }
        }
    }
}