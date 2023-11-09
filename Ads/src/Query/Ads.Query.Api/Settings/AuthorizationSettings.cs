namespace Ads.Query.Api.Settings;

public class AuthorizationSettings
{
    public required Policy[] Policies { get; set; }
}

public class Policy
{
    public required string Name { get; set; }
    public bool RequireAuthenticatedUser { get; set; }
    public RequiredClaim[]? RequiredClaims { get; set; }
    public string[]? RequiredRoles { get; set; }
}

public record RequiredClaim(string ClaimType, string[] Values);