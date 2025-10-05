using Solid.Entities;

namespace Solid.Contracts;

public interface IOrderRepository
{
    public IEnumerable<Order> GetOrdersByCustomerName(string customerName);
    public IEnumerable<Order> GetAllOrders();
    public IEnumerable<Customer> GetAllCustomers();
    
}