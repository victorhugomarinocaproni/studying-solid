using Microsoft.Data.Sqlite;
using Solid.Contracts;
using Solid.Entities;
using Solid.Mappers.OrderItemMappers;

namespace Solid.Repositories;

// Ideally, we would use an ORM, so then we would be able to implement things here without 
// dealing with which database we are working, maintaining the LSP principle.
// Since we are not using an ORM, we had to implement the methods using hard coded SQLite queries.
public class OrderRepository(IDbContext dbContext) : IRepository<Order>, IOrderRepository
{
    public void Add(Order order)
    {
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText =
            "INSERT INTO Orders (customer_name, order_items, total_price, order_status, order_date, customer_category) VALUES (@name, @items, @price, @status, @date, @category)";

        var nameParam = command.CreateParameter();
        nameParam.ParameterName = "@name";
        nameParam.Value = order.CustomerName;
        command.Parameters.Add(nameParam);

        var itemsParam = command.CreateParameter();
        itemsParam.ParameterName = "@items";
        itemsParam.Value = order.Items.ToOrderItemString();
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

    public void Update(Order order)
    {
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = @"
        UPDATE Orders
        SET customer_name = @name,
            order_items = @items,
            total_price = @price,
            order_status = @status,
            order_date = @date,
            customer_category = @category
        WHERE id = @id";

        command.Parameters.Add(new SqliteParameter("@name", order.CustomerName));
        command.Parameters.Add(new SqliteParameter("@items", order.Items.ToOrderItemString()));
        command.Parameters.Add(new SqliteParameter("@price", order.TotalPrice));
        command.Parameters.Add(new SqliteParameter("@status", order.Status));
        command.Parameters.Add(new SqliteParameter("@date", order.OrderDate));
        command.Parameters.Add(new SqliteParameter("@category", order.CustomerCategory));
        command.Parameters.Add(new SqliteParameter("@id", order.Id));

        command.ExecuteNonQuery();
    }

    public IEnumerable<Order> GetOrdersByCustomerName(string customerName)
    {
        var orders = new List<Order>();
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = "SELECT * FROM Orders WHERE customer_name = @name";
        command.Parameters.Add(new SqliteParameter("@name", customerName));
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var order = new Order(
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("customer_name")),
                reader.GetString(reader.GetOrdinal("order_items")).ToOrderItemList(),
                reader.GetFloat(reader.GetOrdinal("total_price")),
                (OrderStatus)reader.GetInt32(reader.GetOrdinal("order_status")),
                DateTime.Parse(reader.GetString(reader.GetOrdinal("order_date"))),
                (CustomerCategory)reader.GetInt32(reader.GetOrdinal("customer_category"))
            );
            orders.Add(order);
        }

        return orders;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        var orders = new List<Order>();
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = "SELECT * FROM Orders";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var order = new Order(
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("customer_name")),
                reader.GetString(reader.GetOrdinal("order_items")).ToOrderItemList(),
                reader.GetFloat(reader.GetOrdinal("total_price")),
                (OrderStatus)reader.GetInt32(reader.GetOrdinal("order_status")),
                DateTime.Parse(reader.GetString(reader.GetOrdinal("order_date"))),
                (CustomerCategory)reader.GetInt32(reader.GetOrdinal("customer_category"))
            );
            orders.Add(order);
        }

        return orders;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customers = new List<Customer>();
        using var command = dbContext.Connection.CreateCommand();
        command.CommandText = "SELECT DISTINCT customer_name, customer_category FROM Orders";
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var customer = new Customer(
                reader.GetString(reader.GetOrdinal("customer_name")),
                (CustomerCategory)reader.GetInt32(reader.GetOrdinal("customer_category"))
            );
            customers.Add(customer);
        }

        return customers;
    }
}