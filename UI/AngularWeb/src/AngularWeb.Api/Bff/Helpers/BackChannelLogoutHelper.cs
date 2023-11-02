using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace AngularWeb.Api.Bff.Helpers;

public static class BackChannelLogoutHelper
{
    private const string BackChannelLogoutScheme = "http://schemas.openid.net/event/backchannel-logout";
    private const string InvalidTokenErrorMessage = "Invalid token";
    private const string EventsClaimType = "events";
    private const string NonceClaimType = "nonce";
    private const string SidClaimType = "sid";
    private const string SubClaimType = "sub";

    public static ClaimsPrincipal GetClaimsPrincipal(
        string logoutJwt,
        DiscoveryDocumentResponse discoResponse,
        string validAudience)
    {
        IEnumerable<SecurityKey> securityKeys = GetSecurityKeys(discoResponse);

        JwtSecurityTokenHandler handler = new();

        handler.InboundClaimTypeMap.Clear();

        TokenValidationParameters parameters = new()
        {
            IssuerSigningKeys = securityKeys,
            RoleClaimType = JwtClaimTypes.Role,
            ValidAudience = validAudience,
            ValidIssuer = discoResponse.Issuer
        };

        var claims = handler.ValidateToken(logoutJwt, parameters, out var _);

        ValidateClaimsPrincipal(claims);

        return claims;
    }

    private static IEnumerable<SecurityKey> GetSecurityKeys(
        DiscoveryDocumentResponse discoResponse)
    {
        var securityKeys = new List<SecurityKey>();

        if (discoResponse.KeySet is not null)
            foreach (var jsonWebKey in discoResponse.KeySet.Keys)
                securityKeys.Add(new JsonWebKey
                {
                    Alg = jsonWebKey.Alg,
                    Crv = jsonWebKey.Crv,
                    E = jsonWebKey.E,
                    Kid = jsonWebKey.Kid,
                    Kty = jsonWebKey.Kty,
                    N = jsonWebKey.N,
                    X = jsonWebKey.X,
                    Y = jsonWebKey.Y
                });

        return securityKeys;
    }

    private static void ValidateClaimsPrincipal(ClaimsPrincipal claims)
    {
        if (claims.FindFirst(SubClaimType) is null)
            throw new Exception(InvalidTokenErrorMessage);

        if (claims.FindFirst(SidClaimType) is null)
            throw new Exception(InvalidTokenErrorMessage);

        var nonce = claims.FindFirstValue(NonceClaimType);

        if (!string.IsNullOrWhiteSpace(nonce))
            throw new Exception(InvalidTokenErrorMessage);

        var eventsJson = claims.FindFirst(EventsClaimType)?.Value;

        if (string.IsNullOrWhiteSpace(eventsJson))
            throw new Exception(InvalidTokenErrorMessage);

        var events = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(eventsJson);

        var logoutEvent = events?.TryGetValue(BackChannelLogoutScheme, out _);

        if (logoutEvent is false)
            throw new Exception(InvalidTokenErrorMessage);
    }
}