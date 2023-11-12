namespace Ads.Query.Api.Settings;

public class AuthenticationSettings
{
    public required string Audience { get; set; }
    public required string Authority { get; set; }
    public required string DefaultScheme { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public bool TokenValidationParametersValidateAudience { get; set; }
    public string[]? TokenValidationParametersValidTypes { get; set; }
}