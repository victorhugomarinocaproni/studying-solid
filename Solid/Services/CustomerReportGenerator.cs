using System.Text;
using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class CustomerReportGenerator(
    ICustomerSpendingCalculator customerSpendingCalculator,
    IOrderRepository orderRepository
) : IReportGenerator
{
    public string GenerateReport()
    {
        var customers = orderRepository.GetAllCustomers();
        var stringBuilder = new StringBuilder();

        Console.WriteLine("=== RELATÓRIO DE CLIENTES ===");

        foreach (var customer in customers)
        {
            PrintTotalSpentPerCustomer(customer);
            stringBuilder.AppendLine($"{customer.Name}, {customer.Category.ToString()}");
        }

        var reportContent = stringBuilder.ToString();
        var path = "relatorio_de_clientes.txt";
        
        SaveReport(path, reportContent);

        return path;
    }

    public void SaveReport(string path, string reportContent)
    {
        File.WriteAllText(path, reportContent);
    }

    private void PrintTotalSpentPerCustomer(Customer customer)
    {
        var customerTotalSpent = customerSpendingCalculator.CalculateTotalSpending(customer.Name);
        Console.WriteLine(
            $"Customer: {customer.Name}({customer.Category.ToString()}) - Total Spent: R${customerTotalSpent:F2}");
    }
}