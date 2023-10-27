using AngularWeb.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AngularWeb.Api.Builders;

public class HttpResponseProblemDetailsBuilder
{
    public static ProblemDetails BuildProblemDetails(
        HttpResponseException exception,
        bool isProductionHosting)
    {
        ProblemDetails details = MapProblemDetails(exception);

        AddErrorCode(exception, details);

        if (!isProductionHosting)
            AddDevelopmentData(exception, details);

        return details;
    }

    private static void AddDevelopmentData(
        HttpResponseException exception,
        ProblemDetails problemDetails)
    {
        var receivedExtensions = exception.ProblemDetails.Extensions;

        problemDetails.Extensions["exception"] = receivedExtensions["exception"];

        problemDetails.Extensions["machineName"] = receivedExtensions["machineName"];

        problemDetails.Extensions["traceId"] = receivedExtensions["traceId"];

        problemDetails.Instance = exception.ProblemDetails.Instance;
    }

    private static void AddErrorCode(
        HttpResponseException exception,
        ProblemDetails problemDetails)
    {
        problemDetails.Extensions["errorCode"] = MapErrorCode(
            exception.ProblemDetails.Extensions["errorCode"]?.ToString());
    }

    private static ProblemDetails MapProblemDetails(
        HttpResponseException exception)
    {
        return new ProblemDetails
        {
            Type = exception.ProblemDetails.Type,
            Title = exception.ProblemDetails.Title,
            Status = exception.ProblemDetails.Status,
            Detail = exception.ProblemDetails.Detail
        };
    }

    private static string MapErrorCode(string? receivedErrorCode)
    {
        return receivedErrorCode switch
        {
            "ExpectedVersionException" => "expected_version",
            "NotFoundException" => "not_found",
            "ValidationException" => "validation",
            _ => "internal_server",
        };
    }
}