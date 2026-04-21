namespace goodburger_api.Application.DTOs;

public sealed record OrderResponse(
    Guid Id,
    IReadOnlyCollection<MenuItemResponse> Items,
    decimal Subtotal,
    decimal DiscountPercentage,
    decimal Discount,
    decimal Total,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);