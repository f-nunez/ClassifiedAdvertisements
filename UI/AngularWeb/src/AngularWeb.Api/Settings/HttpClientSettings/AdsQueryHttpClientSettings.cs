namespace AngularWeb.Api.Settings;

public class AdsQueryHttpClientSettings
{
    public required Uri BaseAddress { get; set; }
    public int HandlerLifetimeInSeconds { get; set; }
    public int TimeoutInSeconds { get; set; }
}