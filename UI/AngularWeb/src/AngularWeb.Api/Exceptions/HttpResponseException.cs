using Microsoft.AspNetCore.Mvc;

namespace AngularWeb.Exceptions;

public class HttpResponseException : Exception
{
    public ProblemDetails ProblemDetails { get; private set; }

    public HttpResponseException(ProblemDetails problemDetails)
    {
        ProblemDetails = problemDetails;
    }
}