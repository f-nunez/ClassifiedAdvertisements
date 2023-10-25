using AngularWeb.Api.HttpClients;
using AngularWeb.Api.Settings;

internal static class AddHttpClientServicesExtension
{
    public static void AddHttpClientServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var adsCommandHttpClientSettings = configuration
            .GetSection(nameof(AdsCommandHttpClientSettings))
            .Get<AdsCommandHttpClientSettings>()!;

        var adsQueryHttpClientSettings = configuration
            .GetSection(nameof(AdsQueryHttpClientSettings))
            .Get<AdsQueryHttpClientSettings>()!;

        services.AddHttpClient<IAdsCommandHttpClient, AdsCommandHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = adsCommandHttpClientSettings.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(adsCommandHttpClientSettings.TimeoutInSeconds);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(adsCommandHttpClientSettings.HandlerLifetimeInSeconds));

        services.AddHttpClient<IAdsQueryHttpClient, AdsQueryHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = adsQueryHttpClientSettings.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(adsQueryHttpClientSettings.TimeoutInSeconds);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(adsQueryHttpClientSettings.HandlerLifetimeInSeconds));
    }
}