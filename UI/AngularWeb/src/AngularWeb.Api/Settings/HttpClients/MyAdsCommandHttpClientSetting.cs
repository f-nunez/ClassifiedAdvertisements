namespace AngularWeb.Api.Settings;

public class MyAdsCommandHttpClientSetting
{
    public required Uri BaseAddress { get; set; }
    public int HandlerLifetimeInSeconds { get; set; }
    public int TimeoutInSeconds { get; set; }
}