using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;
using AngularWeb.Api.Settings;
using Polly.Registry;

namespace AngularWeb.Api.HttpClients;

public class AdsQueryHttpClient : BaseResilienceHttpClient, IAdsQueryHttpClient
{
    public AdsQueryHttpClient(
        HttpClient httpClient,
        ResiliencePipelineProvider<string> resiliencePipelineProvider)
        : base(
            httpClient,
            nameof(HttpClientResilienceStrategySettings),
            resiliencePipelineProvider)
    {
    }

    public async Task<GetMyAdDetailResponse> GetMyAdDetailAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var relativeUri = $"v1/myads/{id}/detail";

        var response = await HttpGetAsync<GetMyAdDetailResponse>(
            relativeUri,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<GetMyAdListResponse> GetMyAdListAsync(
        int skip,
        int take,
        bool sortAsc,
        string sortProp,
        CancellationToken cancellationToken)
    {
        string relativeUri = $"v1/myads?skip={skip}&take={take}&sortasc={sortAsc}&sortprop={sortProp}";

        var response = await HttpGetAsync<GetMyAdListResponse>(
            relativeUri,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<GetMyAdUpdateResponse> GetMyAdUpdateAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var relativeUri = $"v1/myads/{id}/update";

        var response = await HttpGetAsync<GetMyAdUpdateResponse>(
            relativeUri,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }
}