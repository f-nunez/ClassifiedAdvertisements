using Ads.Command.Api.Helpers;
using Ads.Command.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ads.Command.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly IHostEnvironment _hostEnvironment;

    public ApiExceptionFilterAttribute(IHostEnvironment hostEnvironment)
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ExpectedVersionException), HandleExpectedVersionException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(ValidationException), HandleValidationException }
        };

        _hostEnvironment = hostEnvironment;
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
    }

    private void HandleExpectedVersionException(ExceptionContext context)
    {
        ProblemDetails details = ProblemDetailsHelper
            .BuildProblemDetails(context.Exception, _hostEnvironment.IsProduction());

        SetProblemDetailsToExceptionContext(context, details);
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        ProblemDetails details = ProblemDetailsHelper
            .BuildProblemDetails(context.Exception, _hostEnvironment.IsProduction());

        SetProblemDetailsToExceptionContext(context, details);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        ProblemDetails details = ProblemDetailsHelper
            .BuildProblemDetails(context.Exception, _hostEnvironment.IsProduction());

        SetProblemDetailsToExceptionContext(context, details);
    }

    private static void SetProblemDetailsToExceptionContext(
        ExceptionContext context,
        ProblemDetails details)
    {
        context.Result = new ObjectResult(details);
        context.ExceptionHandled = true;
    }
}