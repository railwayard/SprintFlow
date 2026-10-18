
using Microsoft.AspNetCore.Mvc;
using SprintFlowApp.Application.DTOs.Common;
using System.Text.Json;

namespace SprintFlowApp.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "An unhandled exception occurred while processing {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var response = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = "The server could not complete your request."
            };

            response.Extensions["success"] = false;
            response.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}