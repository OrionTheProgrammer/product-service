using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Product_Service.Exceptions;


public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var error = exception switch
        {
            ProductNotFoundException =>
            (
                StatusCodes.Status404NotFound,
                "Producto no encontrado",
                exception.Message
            ),

            PriceValueException =>
            (
                StatusCodes.Status400BadRequest,
                "Precio invalido",
                exception.Message
            ),
            ProductConflictException =>
            (
                StatusCodes.Status409Conflict,
                "Conflicto de producto",
                exception.Message
            ),
            ArgumentException =>
            (
                StatusCodes.Status400BadRequest,
                "Solicitud invalida",
                exception.Message
            ),
            _ =>
            (
                StatusCodes.Status500InternalServerError,
                "Error interno del servidor",
                "Ocurrio un error inesperado."
            )
        };

        int statusCode = error.Item1;
        string title = error.Item2;
        string detail = error.Item3;

        httpContext.Response.StatusCode = statusCode;

        ProblemDetails problem = new()
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        if (statusCode >= 500)
        {
            _logger.LogError(
                exception,
                "Error inesperado, procesando {Path}",
                httpContext.Request.Path
            );
        }
        else
        {
            _logger.LogWarning(
                "Error controlado {Status}: {Message}",
                statusCode,
                exception.Message
            );
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
            Exception = exception
        });

    }
}
