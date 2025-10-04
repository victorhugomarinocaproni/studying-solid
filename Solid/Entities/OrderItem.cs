namespace Solid.Entities;

public class OrderItem(
    string productName, 
    float productPrice, 
    int quantity, 
    Discount discount)
{
    public string ProductName { get; } = productName;
    public float ProductPrice { get;} = productPrice;
    public int Quantity { get; } = quantity;
    public  Discount Discount { get; } = discount;
}