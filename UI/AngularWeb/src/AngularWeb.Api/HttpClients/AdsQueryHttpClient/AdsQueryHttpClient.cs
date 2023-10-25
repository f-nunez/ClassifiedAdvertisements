using System.Text.Json;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;
using AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;

namespace AngularWeb.Api.HttpClients;

public class AdsQueryHttpClient : IAdsQueryHttpClient
{
    private static readonly JsonSerializerOptions s_serializerOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public AdsQueryHttpClient(HttpClient httpClient)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient));

        _httpClient = httpClient;
    }

    public async Task<GetMyAdDetailResponse> GetMyAdDetailAsync(string id, CancellationToken cancellationToken)
    {
        var uri = $"v1/myads/{id}/detail";
        var request = CreateRequest(HttpMethod.Get, uri);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<GetMyAdDetailResponse>(
            contentStream,
            s_serializerOptions,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<GetMyAdListResponse> GetMyAdListAsync(int skip, int take, bool sortAsc, string sortProp, CancellationToken cancellationToken)
    {
        string uri = $"v1/myads?skip={skip}&take={take}&sortasc={sortAsc}&sortprop={sortProp}";
        var request = CreateRequest(HttpMethod.Get, uri);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<GetMyAdListResponse>(
            contentStream,
            s_serializerOptions,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<GetMyAdUpdateResponse> GetMyAdUpdateAsync(string id, CancellationToken cancellationToken)
    {
        var uri = $"v1/myads/{id}/update";
        var request = CreateRequest(HttpMethod.Get, uri);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<GetMyAdUpdateResponse>(
            contentStream,
            s_serializerOptions,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    private static HttpRequestMessage CreateRequest(HttpMethod httpMethod, string? requestUri)
    {
        return new HttpRequestMessage(httpMethod, requestUri);
    }
}