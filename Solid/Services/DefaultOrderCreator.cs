using Solid.Contracts;
using Solid.Entities;

namespace Solid.Services;

public class DefaultOrderCreator(
    DiscountService discountService
    ) : IOrderCreator
{
    public Order CreateNewOrder(
        string customerName,
        List<OrderItem> items,
        CustomerCategory customerCategory)
    {
        var orderTotalPrice = 0f;
        foreach (var item in items)
        {
            orderTotalPrice += item.ProductPrice * item.Quantity;
        }

        var order = new Order(
            customerName,
            items,
            orderTotalPrice,
            OrderStatus.Pending,
            DateTime.Now, 
            customerCategory
            );
        
        discountService.ApplyOrderDiscount(order);
        
        // VIP Customers receive a 5% discount in Default Orders.
        discountService.ApplyVipCustomerDiscount(order);
        
        return order;
    }
}