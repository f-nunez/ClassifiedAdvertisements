using System.Security.Claims;
using AngularWeb.Api.Bff.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AngularWeb.Api.Controllers;

[Route("[controller]")]
public class BffController : ControllerBase
{
    private readonly IOptions<AuthenticationOptions> _authenticationOptions;

    public BffController(IOptions<AuthenticationOptions> authenticationOptions)
    {
        _authenticationOptions = authenticationOptions;
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
}