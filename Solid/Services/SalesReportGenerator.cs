using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class SalesReportGenerator(IOrderRepository orderRepository) : IReportGenerator
{
    public string GenerateReport()
    {
        var orders = orderRepository.GetAllOrders();
        
        Console.WriteLine("=== RELATÓRIO DE VENDAS ===");
        
        var totalSales = CalculateTotalSales(orders);
        
        Console.WriteLine($"General Total: {totalSales}");

        var reportContent = $"=== RELATÓRIO DE VENDAS ===\n";
        reportContent += $"Total Sales: {totalSales:F2}";

        var path = "relatorio_de_vendas.txt";
        
        SaveReport(path, reportContent);
        
        return path;
    }
    private static float CalculateTotalSales(IEnumerable<Order> orders)
    {
        var totalSales = 0f;
        foreach (var order in orders)
        {
            Console.WriteLine($"Order #{order.Id} - Customer: {order.CustomerName} - Total: {order.TotalPrice:F2} - Status: {order.Status.ToString()}");
            totalSales += order.TotalPrice;
        }
        return totalSales;
    }

    public void SaveReport(string path, string reportContent)
    {
        File.WriteAllText(path, reportContent);
    }
}