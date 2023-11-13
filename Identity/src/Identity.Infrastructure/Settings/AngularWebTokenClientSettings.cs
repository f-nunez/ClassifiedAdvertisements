namespace Identity.Infrastructure.Settings;

public class AngularWebTokenClientSettings
{
    public List<string> AllowedCorsOrigins { get; set; } = new();
    public List<string> PostLogoutRedirectUris { get; set; } = new();
    public List<string> RedirectUris { get; set; } = new();
}