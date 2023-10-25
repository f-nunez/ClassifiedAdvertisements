using System.Text;
using System.Text.Json;
using AngularWeb.Api.Application.Features.MyAds.Commands.CreateMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.DeleteMyAd;
using AngularWeb.Api.Application.Features.MyAds.Commands.UpdateMyAd;

namespace AngularWeb.Api.HttpClients;

public class AdsCommandHttpClient : IAdsCommandHttpClient
{
    private static readonly JsonSerializerOptions s_serializerOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public AdsCommandHttpClient(HttpClient httpClient)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient));

        _httpClient = httpClient;
    }

    public async Task<CreateMyAdResponse> CreateMyAdAsync(string? description, string? title, CancellationToken cancellationToken)
    {
        var uri = "v1/myads";
        var request = CreateRequest(HttpMethod.Post, uri);
        object payload = new { description, title };
        request.Content = ToStringContent(payload);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<CreateMyAdResponse>(
            contentStream,
            s_serializerOptions,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<DeleteMyAdResponse> DeleteMyAdAsync(string? id, long version, CancellationToken cancellationToken)
    {
        var uri = $"v1/myads/{id}/version/{version}";
        var request = CreateRequest(HttpMethod.Delete, uri);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<DeleteMyAdResponse>(
            contentStream,
            s_serializerOptions,
            cancellationToken
        );

        return response ?? throw new ArgumentNullException(nameof(response));
    }

    public async Task<UpdateMyAdResponse> UpdateMyAdAsync(string? id, string? description, string? title, long version, CancellationToken cancellationToken)
    {
        var uri = $"v1/myads/{id}";
        var request = CreateRequest(HttpMethod.Put, uri);
        object payload = new { description, title, version };
        request.Content = ToStringContent(payload);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<UpdateMyAdResponse>(
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

    private static StringContent ToStringContent(object obj)
    {
        return new StringContent(
            JsonSerializer.Serialize(obj),
            Encoding.UTF8, "application/json"
        );
    }
}