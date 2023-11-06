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
}