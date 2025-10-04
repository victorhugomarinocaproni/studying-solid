using System.Data.Common;
using Microsoft.Data.Sqlite;
using Solid.Contracts;

namespace Solid.Database;

public class SqliteDbContext : IDbContext
{
    public required DbConnection Connection { get; set; }

    public void Connect()
    {
        Connection = new SqliteConnection("DataSource=loja.db");
        Connection.Open();
        
        using var command = Connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Orders (
                id INTEGER PRIMARY KEY,
                customer_name TEXT,
                order_items TEXT,
                total_price REAL,
                order_status INTEGER,
                order_date TEXT,
                customer_category INTEGER
            )";
        command.ExecuteNonQuery();
    }
}