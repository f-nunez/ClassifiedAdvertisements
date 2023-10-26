namespace AngularWeb.Api.Settings;

public abstract class BaseHttpClientSettings
{
    public required Uri BaseAddress { get; set; }
    public TimeSpan HandlerLifetime { get; set; } = TimeSpan.FromSeconds(120);
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}