using System.Text.Json;
using goodburger_api.API.Contracts;
using goodburger_api.Application.Exceptions;

namespace goodburger_api.API.Middlewares;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainValidationException ex)
        {
            await WriteErrorAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (NotFoundException ex)
        {
            await WriteErrorAsync(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro não tratado durante o processamento da requisição.");
            await WriteErrorAsync(context, StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.");
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var payload = new ApiErrorResponse(statusCode, message);

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}