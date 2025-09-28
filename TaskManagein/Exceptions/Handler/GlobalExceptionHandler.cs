using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using TaskManagein.Models;

namespace TaskManagein.Exceptions.Handler
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly Dictionary<Type, Func<HttpContext, Exception, CancellationToken, ValueTask<bool>>> _exceptionHandlers;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, CancellationToken, ValueTask<bool>>>
    {
        { typeof(InvalidFieldException), (context, exception, cancellationToken) => HandleUniqueFieldValuesExceptionAsync(context, (InvalidFieldException)exception, cancellationToken) },
        { typeof(ResourceNotFoundException), (context, exception, cancellationToken) => HandleResourceNotFoundExceptionAsync(context, (ResourceNotFoundException)exception, cancellationToken) }
    };
        }


        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            if (_exceptionHandlers.TryGetValue(exception.GetType(), out var handler))
            {
                return await handler(httpContext, exception, cancellationToken);
            }

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error"
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private async ValueTask<bool> HandleMinhaExcecaoEspecificaAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Erro específico",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private async ValueTask<bool> HandleUniqueFieldValuesExceptionAsync(
            HttpContext httpContext,
            InvalidFieldException exception,
            CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>(exception.Message, null, exception.Errors);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response
                .WriteAsJsonAsync(response, cancellationToken);

            return true;
        }

        private async ValueTask<bool> HandleResourceNotFoundExceptionAsync(
            HttpContext httpContext,
            ResourceNotFoundException exception,
            CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>(exception.Message, null, null);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response
                .WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
