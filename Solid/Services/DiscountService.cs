using Solid.Entities;

namespace Solid.Services;

public class DiscountService
{
    public void ApplyOrderDiscount(Order order)
    {
        var orderTotalPriceWithDiscount = 0f;
        foreach (var orderItem in order.Items)
        {
            var discount = GetItemDiscount(orderItem.Discount);
            orderTotalPriceWithDiscount += orderItem.ProductPrice * orderItem.Quantity * discount;
        }
        order.TotalPrice = orderTotalPriceWithDiscount;
    }
    
    private static float GetItemDiscount(Discount discount)
    {
        return discount switch
        {
            Discount.Normal => 1,
            Discount.TenPercent => 0.9f,
            Discount.TwentyPercent => 0.8f,
            _ => throw new ArgumentOutOfRangeException(nameof(discount), discount, "Invalid discount type.")
        };
    }

    public void ApplyVipCustomerDiscount(Order order)
    {
        order.TotalPrice *= 0.95f;
    }
}