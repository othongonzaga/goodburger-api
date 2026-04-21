namespace goodburger_api.Application.DTOs;

public sealed record UpsertOrderRequest(
    IReadOnlyCollection<string> Items);