using Solid.Contracts;
using Solid.Entities;
using Solid.Exceptions;

namespace Solid.Services;

public class InventoryService(
    IInventoryStrategy inventoryStrategy)
{

    public bool ValidateInventory(Inventory inventory, List<OrderItem> orderItems)
    {
        try
        {
            inventoryStrategy.Validate(inventory, orderItems);
        }
        catch (EntityNotFoundException)
        {
            return false;
        }
        catch (InsufficientStockException)
        {
            return false;
        }

        return true;
    }
}