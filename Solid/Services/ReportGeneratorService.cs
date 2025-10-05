using Solid.Contracts;

namespace Solid.Services;

public class ReportGeneratorService(
    IReportGenerator reportGenerator)
{
    public void GenerateReport()
    {
        var path = reportGenerator.GenerateReport();
        Console.WriteLine($"Report saved at: {path}");
    }
}