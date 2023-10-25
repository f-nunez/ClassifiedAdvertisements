namespace AngularWeb.Api.Settings;

public class MyAdsQueryHttpClientSetting
{
    public required Uri BaseAddress { get; set; }
    public int HandlerLifetimeInSeconds { get; set; }
    public int TimeoutInSeconds { get; set; }
}