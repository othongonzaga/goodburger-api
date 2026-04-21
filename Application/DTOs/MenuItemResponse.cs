namespace goodburger_api.Application.DTOs;

public sealed record MenuItemResponse(
    string Code,
    string Name,
    string Category,
    decimal Price);