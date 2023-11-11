using AngularWeb.Api.Bff.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace AngularWeb.Api.Bff.LogoutSessionManagement;

public class LogoutSessionCookieAuthenticationEvent : CookieAuthenticationEvents
{
    private const string SidClaimType = "sid";
    private const string SubClaimType = "sub";
    private readonly IOptions<AuthenticationOptions> _authenticationOptions;
    private readonly ILogger<LogoutSessionCookieAuthenticationEvent> _logger;
    private readonly ILogoutSessionService _logoutSessionService;

    public LogoutSessionCookieAuthenticationEvent(
        IOptions<AuthenticationOptions> authenticationOptions,
        ILogger<LogoutSessionCookieAuthenticationEvent> logger,
        ILogoutSessionService logoutSessionService)
    {
        _authenticationOptions = authenticationOptions;
        _logger = logger;
        _logoutSessionService = logoutSessionService;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        if (context?.Principal?.Identity?.IsAuthenticated == true)
        {
            var sub = context!.Principal!.FindFirst(SubClaimType)?.Value;
            var sid = context!.Principal!.FindFirst(SidClaimType)?.Value;

            string sessionKey = LogoutSessionKeyHelper.GetSessionKey(sub, sid);

            if (await _logoutSessionService.IsLoggedOutAsync(sub, sid))
            {
                _logger.LogInformation($"Proceeding to signout the {nameof(sessionKey)}: {sessionKey}");

                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(_authenticationOptions.Value.DefaultScheme);
            }
        }
    }
}