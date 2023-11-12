namespace AngularWeb.Api.Settings;

public class BffTokenBasedAuthenticationSettings
{
    public string AuthenticationDefaultScheme { get; set; } = string.Empty;
    public string JwtBearerAudience { get; set; } = string.Empty;
    public string JwtBearerAuthority { get; set; } = string.Empty;
    public bool JwtBearerRequireHttpsMetadata { get; set; }
    public string JwtBearerTokenValidationParametersNameClaimType { get; set; } = string.Empty;
    public string JwtBearerTokenValidationParametersRoleClaimType { get; set; } = string.Empty;
    public bool JwtBearerTokenValidationParametersValidateAudience { get; set; }
}