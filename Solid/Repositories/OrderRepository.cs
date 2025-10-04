using Microsoft.Data.Sqlite;
using Solid.Contracts;
using Solid.Entities;
using Solid.Mappers.OrderItemMappers;

namespace Solid.Repositories;

// Ideally, we would use an ORM, so then we would be able to implement things here without 
// dealing with which database we are working, maintaining the LSP principle.
// Since we are not using an ORM, we had to implement the methods using hard coded SQLite queries.
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
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = "SELECT * FROM Orders WHERE id = @id";
        command.Parameters.Add(new SqliteParameter("@id", id));
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Order(
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("customer_name")),
                reader.GetString(reader.GetOrdinal("order_items")).ToOrderItemList(),
                reader.GetFloat(reader.GetOrdinal("total_price")),
                (OrderStatus)reader.GetInt32(reader.GetOrdinal("order_status")),
                DateTime.Parse(reader.GetString(reader.GetOrdinal("order_date"))),
                (CustomerCategory)reader.GetInt32(reader.GetOrdinal("customer_category"))
            );
        }

        return null;
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