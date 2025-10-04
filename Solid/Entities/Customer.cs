namespace Solid.Entities;

public class Customer(
    string name,
    CustomerCategory category
    )
{
    public string Name { get; } = name;
    public CustomerCategory Category { get; } = category;
}