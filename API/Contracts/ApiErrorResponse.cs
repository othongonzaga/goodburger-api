namespace goodburger_api.API.Contracts;

public sealed record ApiErrorResponse(
    int StatusCode,
    string Message);