using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class BoletoPaymentProcessor : IPaymentProcessor, IDocumentGenerable
{
    public void Process(Order order)
    {
        Console.WriteLine("Generating Boleto...");
        GenerateDocument();
        // Boleto is not processed instantly, it takes some time...
    }

    public void GenerateDocument()
    {
        // Just a simulation
    }
}