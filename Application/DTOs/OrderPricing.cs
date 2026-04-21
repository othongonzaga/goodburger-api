namespace goodburger_api.Application.DTOs;

public sealed record OrderPricing(
    decimal Subtotal,
    decimal DiscountPercentage,
    decimal Discount,
    decimal Total);