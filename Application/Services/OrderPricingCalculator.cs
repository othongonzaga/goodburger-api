using goodburger_api.Application.DTOs;
using goodburger_api.Application.Interfaces;
using goodburger_api.Domain.ValueObjects;

namespace goodburger_api.Application.Services;

public sealed class OrderPricingCalculator(IMenuCatalog menuCatalog) : IOrderPricingCalculator
{
    public OrderPricing Calculate(OrderSelection selection)
    {
        var items = menuCatalog.GetItems(selection);
        var subtotal = items.Sum(item => item.Price);

        var discountPercentage =
            selection.HasSandwich && selection.HasSide && selection.HasDrink ? 0.20m :
            selection.HasSandwich && selection.HasDrink ? 0.15m :
            selection.HasSandwich && selection.HasSide ? 0.10m :
            0m;

        var discount = Math.Round(subtotal * discountPercentage, 2, MidpointRounding.AwayFromZero);
        var total = subtotal - discount;

        return new OrderPricing(subtotal, discountPercentage, discount, total);
    }
}