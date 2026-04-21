namespace goodburger_api.Application.Exceptions;

public sealed class NotFoundException(string message) : Exception(message)
{
}