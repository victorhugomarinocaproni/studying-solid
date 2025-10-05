namespace Solid.Entities;

public class InventoryItem(
    string productName,
    int quantity)
{
    public string ProductName { get; set; } =  productName;
    public int Quantity { get; set; } = quantity;
}