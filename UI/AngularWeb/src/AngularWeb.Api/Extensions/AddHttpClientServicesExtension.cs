using System.Net.Sockets;
using AngularWeb.Api.HttpClients;
using AngularWeb.Api.Settings;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

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

        var httpClientResilienceStrategySettings = configuration
            .GetSection(nameof(HttpClientResilienceStrategySettings))
            .Get<HttpClientResilienceStrategySettings>()!;

        services.AddHttpClient<IAdsCommandHttpClient, AdsCommandHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = adsCommandHttpClientSettings.BaseAddress;
                httpClient.Timeout = adsCommandHttpClientSettings.Timeout;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(adsCommandHttpClientSettings.HandlerLifetime);

        services.AddHttpClient<IAdsQueryHttpClient, AdsQueryHttpClient>()
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = adsQueryHttpClientSettings.BaseAddress;
                httpClient.Timeout = adsQueryHttpClientSettings.Timeout;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .SetHandlerLifetime(adsQueryHttpClientSettings.HandlerLifetime);

        services.AddResiliencePipeline(nameof(HttpClientResilienceStrategySettings), (builder, context) =>
        {
            builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions());

            builder.AddRetry(new RetryStrategyOptions
            {
                BackoffType = httpClientResilienceStrategySettings.BackoffType,
                Delay = httpClientResilienceStrategySettings.Delay,
                MaxRetryAttempts = httpClientResilienceStrategySettings.MaxRetryAttempts,
                ShouldHandle = new PredicateBuilder()
                    .Handle<SocketException>()
                    .Handle<InvalidOperationException>()
                    .Handle<HttpRequestException>()
                    .Handle<TimeoutException>()
                    .Handle<BrokenCircuitException>(),
                UseJitter = httpClientResilienceStrategySettings.UseJitter
            });
        });
    }
}