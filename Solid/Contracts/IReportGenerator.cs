namespace Solid.Contracts;

public interface IReportGenerator
{
    public string GenerateReport();
    public void SaveReport(string path, string reportContent);
}