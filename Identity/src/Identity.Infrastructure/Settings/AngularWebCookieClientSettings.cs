namespace Identity.Infrastructure.Settings;

public class AngularWebCookieClientSettings
{
    public List<string> AllowedCorsOrigins { get; set; } = new();
    public List<string> PostLogoutRedirectUris { get; set; } = new();
    public List<string> RedirectUris { get; set; } = new();
    public string? BackChannelLogoutUri { get; set; }
}