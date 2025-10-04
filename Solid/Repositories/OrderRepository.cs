using Solid.Contracts;
using Solid.Entities;

namespace Solid.Repositories;

public class OrderRepository(IDbContext dbContext) : IRepository<Order>
{
    public void Add(Order order)
    {
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = "INSERT INTO Orders (customer_name, order_items, total_price, order_status, order_date, customer_category) VALUES (@name, @items, @price, @status, @date, @category)";
        
        var nameParam = command.CreateParameter();
        nameParam.ParameterName = "@name";
        nameParam.Value = order.CustomerName;
        command.Parameters.Add(nameParam);

        var itemsParam = command.CreateParameter();
        itemsParam.ParameterName = "@items";
        itemsParam.Value = order.Items;
        command.Parameters.Add(itemsParam);

        var priceParam = command.CreateParameter();
        priceParam.ParameterName = "@price";
        priceParam.Value = order.TotalPrice;
        command.Parameters.Add(priceParam);

        var statusParam = command.CreateParameter();
        statusParam.ParameterName = "@status";
        statusParam.Value = order.Status;
        command.Parameters.Add(statusParam);

        var dateParam = command.CreateParameter();
        dateParam.ParameterName = "@date";
        dateParam.Value = order.OrderDate;
        command.Parameters.Add(dateParam);

        var categoryParam = command.CreateParameter();
        categoryParam.ParameterName = "@category";
        categoryParam.Value = order.CustomerCategory;
        command.Parameters.Add(categoryParam);

        command.ExecuteNonQuery();
    }

    public Order? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Order order)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}