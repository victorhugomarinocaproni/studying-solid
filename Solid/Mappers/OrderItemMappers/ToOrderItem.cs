using System.Text.Json;
using Solid.Entities;

namespace Solid.Mappers.OrderItemMappers;

public static class OrderItemMappers
{
    public static List<OrderItem> ToOrderItemList(this string orderItemsJson) =>
        string.IsNullOrWhiteSpace(orderItemsJson)
            ? new List<OrderItem>()
            : JsonSerializer.Deserialize<List<OrderItem>>(orderItemsJson) ?? new List<OrderItem>();
    
    public static string ToOrderItemString(this List<OrderItem> orderItems) =>
        JsonSerializer.Serialize(orderItems);
}