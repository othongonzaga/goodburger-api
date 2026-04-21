namespace goodburger_api.Application.Exceptions;

public sealed class DomainValidationException(string message) : Exception(message)
{
}