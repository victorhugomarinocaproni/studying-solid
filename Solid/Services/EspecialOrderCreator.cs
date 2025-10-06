using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class EspecialOrderCreator(DiscountService discountService) : IOrderCreator
{
    public Order CreateNewOrder(
        string customerName,
        List<OrderItem> orderItems,
        CustomerCategory customerCategory)
    {
        
        const float specialTax = 1.15f;
        var orderTotalPrice = 0f;
        
        foreach (var item in orderItems)
        {
            orderTotalPrice += item.ProductPrice * item.Quantity;
        }

        var order = new Order(
            customerName,
            orderItems,
            orderTotalPrice,
            OrderStatus.Pending,
            DateTime.Now, 
            customerCategory
        );
        
        discountService.ApplyOrderDiscount(order);
        
        // Especial Orders have an additional tax of 15%
        order.TotalPrice += order.TotalPrice * specialTax;
        
        return order;
    }
}