using Microsoft.AspNetCore.Mvc;

namespace AngularWeb.Api.Builders;

public class GlobalProblemDetailsBuilder
{
    public static ProblemDetails BuildProblemDetails(
        HttpContext httpContext,
        Exception exception,
        bool isProductionHosting)
    {
        ProblemDetails details = GetProblemDetails(exception);

        AddErrorCode(exception, details);

        if (!isProductionHosting)
            AddDevelopmentData(httpContext, exception, details);

        return details;
    }

    private static void AddDevelopmentData(
        HttpContext httpContext,
        Exception exception,
        ProblemDetails problemDetails)
    {
        problemDetails.Extensions["exception"] = exception.ToString();

        problemDetails.Extensions["machineName"] = Environment.MachineName;

        string? traceId = httpContext.TraceIdentifier;

        if (!string.IsNullOrEmpty(traceId))
            problemDetails.Extensions["traceId"] = traceId;

        string? instance = httpContext.Request.Path.Value;

        if (!string.IsNullOrEmpty(instance))
            problemDetails.Instance = instance;
    }

    private static void AddErrorCode(
        Exception exception,
        ProblemDetails problemDetails)
    {
        problemDetails.Extensions["errorCode"] = exception.GetType().Name;
    }

    private static ProblemDetails GetProblemDetails(Exception exception)
    {
        return exception.GetType().Name switch
        {
            // Api exceptions
            _ => MapProblemDetails(exception)
        };
    }

    private static ProblemDetails MapProblemDetails(Exception exception)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "The server encountered an unexpected condition that prevented it from fulfilling the request.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };
    }
}