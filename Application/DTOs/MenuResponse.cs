namespace goodburger_api.Application.DTOs;

public sealed record MenuResponse(
    IReadOnlyCollection<MenuItemResponse> Sandwiches,
    IReadOnlyCollection<MenuItemResponse> Sides,
    IReadOnlyCollection<MenuItemResponse> Drinks);