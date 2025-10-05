namespace Solid.Entities;

public class Inventory(
    List<InventoryItem> inventoryItems)
{
    public List<InventoryItem> InventoryItems { get; set; } = inventoryItems;
}