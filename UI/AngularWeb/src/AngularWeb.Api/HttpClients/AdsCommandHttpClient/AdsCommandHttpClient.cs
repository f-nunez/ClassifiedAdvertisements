using AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;
using AngularWeb.Api.Settings;
using Polly.Registry;

namespace AngularWeb.Api.HttpClients;

public class AdsCommandHttpClient : BaseResilienceHttpClient, IAdsCommandHttpClient
{
    public AdsCommandHttpClient(
        HttpClient httpClient,
        ResiliencePipelineProvider<string> resiliencePipelineProvider)
        : base(
            httpClient,
            nameof(HttpClientResilienceStrategySettings),
            resiliencePipelineProvider)
    {
    }

    public async Task<CreateMyAdResponse> CreateMyAdAsync(
        string? description,
        string? title,
        CancellationToken cancellationToken)
    {
        var relativeUri = "v1/myads";

        object payload = new { description, title };

        var response = await HttpPostAsync<CreateMyAdResponse>(
            relativeUri,
            payload,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<DeleteMyAdResponse> DeleteMyAdAsync(
        string? id,
        long version,
        CancellationToken cancellationToken)
    {
        var relativeUri = $"v1/myads/{id}/version/{version}";

        var response = await HttpDeleteAsync<DeleteMyAdResponse>(
            relativeUri,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<UpdateMyAdResponse> UpdateMyAdAsync(
        string? id,
        string? description,
        string? title,
        long version,
        CancellationToken cancellationToken)
    {
        var relativeUri = $"v1/myads/{id}";

        object payload = new { description, title, version };

        var response = await HttpPutAsync<UpdateMyAdResponse>(
            relativeUri,
            payload,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }
}