namespace Solid.Contracts;

public interface ICustomerSpendingCalculator
{
    float CalculateTotalSpending(string customerName);
}