using AngularWeb.Api.HttpClients;
using AngularWeb.Api.Settings;

internal static class AddHttpClientServicesExtension
{
    public static void AddHttpClientServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var myAdsCommandHttpClientSetting = configuration
            .GetSection(nameof(MyAdsCommandHttpClientSetting))
            .Get<MyAdsCommandHttpClientSetting>()!;

        var myAdsQueryHttpClientSetting = configuration
            .GetSection(nameof(MyAdsQueryHttpClientSetting))
            .Get<MyAdsQueryHttpClientSetting>()!;

        services.AddHttpClient<IAdsCommandHttpClient, AdsCommandHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = myAdsCommandHttpClientSetting.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(myAdsCommandHttpClientSetting.TimeoutInSeconds);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(myAdsCommandHttpClientSetting.HandlerLifetimeInSeconds));

        services.AddHttpClient<IAdsQueryHttpClient, AdsQueryHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = myAdsQueryHttpClientSetting.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(myAdsQueryHttpClientSetting.TimeoutInSeconds);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(myAdsQueryHttpClientSetting.HandlerLifetimeInSeconds));
    }
}