using System.Security.Claims;
using AngularWeb.Api.Bff.Common;
using AngularWeb.Api.Bff.LogoutSessionManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AngularWeb.Api.Controllers;

[Route("[controller]")]
public class BffController : ControllerBase
{
    private readonly IOptions<AuthenticationOptions> _authenticationOptions;
    private readonly ILogoutSessionService _logoutSessionService;

    public BffController(
        IOptions<AuthenticationOptions> authenticationOptions,
        ILogoutSessionService logoutSessionService)
    {
        _authenticationOptions = authenticationOptions;
        _logoutSessionService = logoutSessionService;
    }

    [AllowAnonymous]
    [HttpPost("BackChannelLogout")]
    public async Task<IActionResult> BackChannelLogout(string logout_token)
    {
        Response.Headers.Add("Cache-Control", "no-cache, no-store");

        Response.Headers.Add("Pragma", "no-cache");

        await _logoutSessionService.ProcessBackChannelLogout(logout_token);

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("User")]
    [Produces("application/json")]
    public ActionResult GetUserInfo()
    {
        List<ClaimRecord> claims = new();

        if (User.Identity?.IsAuthenticated == true)
            claims = ((ClaimsIdentity)User.Identity).Claims
                .Select(c => new ClaimRecord(c.Type, c.Value))
                .ToList();

        return new JsonResult(claims);
    }

    [AllowAnonymous]
    [HttpGet("Login")]
    public IActionResult Login(string? redirectUri)
    {
        if (string.IsNullOrEmpty(redirectUri))
            redirectUri = "/";

        //TODO: validate redirectUri with white list

        var challengeScheme = _authenticationOptions.Value
            .DefaultChallengeScheme!;

        var authProperties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };

        return Challenge(authProperties, challengeScheme);
    }

    [HttpGet("Logout")]
    public IActionResult Logout(string? redirectUri)
    {
        if (string.IsNullOrEmpty(redirectUri))
            redirectUri = "/";

        //TODO: validate redirectUri with white list

        var authSchemes = _authenticationOptions.Value.Schemes
            .Select(authScheme => authScheme.Name)
            .ToArray();

        var authProperties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };

        return SignOut(authProperties, authSchemes);
    }

    [AllowAnonymous]
    [HttpGet("GetAuthProperties")]
    [Produces("application/json")]
    public ActionResult GetAuthProperties()
    {
        var props = new List<KeyValuePair<string, string?>>();
        var authFeatures = HttpContext.Features.Get<IAuthenticateResultFeature>();
        var authProps = authFeatures?.AuthenticateResult?.Properties;

        if (authProps is not null)
            foreach (var prop in authProps.Items)
                props.Add(prop);

        return new JsonResult(props);
    }
}