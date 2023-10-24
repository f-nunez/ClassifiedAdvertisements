using System.Text;
using System.Text.Json;
using Ads.Command.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Command.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IHostEnvironment hostEnvironment)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            var problemDetails = ProblemDetailsHelper.BuildProblemDetails(
                httpContext,
                exception,
                hostEnvironment.IsProduction()
            );

            await SetResponseAsync(httpContext, problemDetails);
        }
    }

    private static async Task SetResponseAsync(
        HttpContext httpContext,
        ProblemDetails problemDetails)
    {
        httpContext.Response.StatusCode = problemDetails.Status
            ?? StatusCodes.Status500InternalServerError;

        SetResponseContentType(httpContext);

        SetResponseHeaders(httpContext);

        string responseBody = JsonSerializer.Serialize(problemDetails);

        await httpContext.Response.WriteAsync(responseBody);
    }

    private static void SetResponseContentType(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/problem+json";
    }

    private static void SetResponseHeaders(HttpContext httpContext)
    {
        httpContext.Response.Headers["cache-control"] = "no-cache, no-store";

        httpContext.Response.Headers["charset"] = Encoding.UTF8.WebName;

        httpContext.Response.Headers["expires"] = "-1";

        httpContext.Response.Headers["pragma"] = "no-cache";
    }
}