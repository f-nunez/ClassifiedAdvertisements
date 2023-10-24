using Ads.Command.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Command.Api.Helpers;

public static class ProblemDetailsHelper
{
    public static ProblemDetails BuildProblemDetails(
        HttpContext httpContext,
        Exception exception,
        bool isProductionHosting)
    {
        ProblemDetails details = GetProblemDetails(exception);

        AddErrorCodeExtensionMember(exception, details);

        if (!isProductionHosting)
            AddDevelopmentExtensionMembers(httpContext, exception, details);

        return details;
    }

    private static void AddDevelopmentExtensionMembers(
        HttpContext httpContext,
        Exception exception,
        ProblemDetails problemDetails)
    {
        problemDetails.Extensions.Add("exception", exception.ToString());

        problemDetails.Extensions.Add("machineName", Environment.MachineName);

        string? traceId = httpContext.TraceIdentifier;

        if (!string.IsNullOrEmpty(traceId))
            problemDetails.Extensions["traceId"] = traceId;

        string? instance = httpContext.Request.Path.Value;

        if (!string.IsNullOrEmpty(instance))
            problemDetails.Extensions["instance"] = instance;
    }

    private static void AddErrorCodeExtensionMember(
        Exception exception,
        ProblemDetails problemDetails)
    {
        problemDetails.Extensions.Add("errorCode", exception.GetType().Name);
    }

    private static ProblemDetails GetProblemDetails(Exception exception)
    {
        return exception.GetType().Name switch
        {
            nameof(ExpectedVersionException) => MapProblemDetails((ExpectedVersionException)exception),
            nameof(NotFoundException) => MapProblemDetails((NotFoundException)exception),
            nameof(ValidationException) => MapProblemDetails((ValidationException)exception),
            _ => MapProblemDetails(exception),
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

    private static ProblemDetails MapProblemDetails(ExpectedVersionException exception)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            Title = "The request could not be completed.",
            Status = StatusCodes.Status409Conflict,
            Detail = exception.Message
        };
    }

    private static ProblemDetails MapProblemDetails(NotFoundException exception)
    {
        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Status = StatusCodes.Status404NotFound,
            Detail = exception.Message
        };
    }

    private static ProblemDetails MapProblemDetails(ValidationException exception)
    {
        return new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Title = "The request was invalid.",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message
        };
    }
}