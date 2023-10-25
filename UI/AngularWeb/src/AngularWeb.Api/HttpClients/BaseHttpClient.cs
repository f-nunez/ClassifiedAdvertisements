using System.Text.Json;

namespace AngularWeb.Api.HttpClients;

public abstract class BaseHttpClient
{
    private static readonly JsonSerializerOptions s_serializerOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public BaseHttpClient(HttpClient httpClient)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient));

        if (httpClient.BaseAddress is null)
            throw new ArgumentNullException(nameof(httpClient.BaseAddress));

        _httpClient = httpClient;
    }

    protected async Task HttpDeleteAsync(
        string requestUri,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(HttpMethod.Delete, requestUri);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );
    }

    protected async Task<T?> HttpDeleteAsync<T>(
        string requestUri,
        CancellationToken cancellationToken) where T : class
    {
        var request = CreateRequest(HttpMethod.Delete, requestUri);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        using var contentStream = await result.Content
            .ReadAsStreamAsync(cancellationToken);

        return await DeserializeAsync<T>(contentStream, cancellationToken);
    }

    protected async Task<string> HttpGetAsync(
        string requestUri,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(HttpMethod.Get, requestUri);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        return await result.Content.ReadAsStringAsync(cancellationToken);
    }

    protected async Task<T?> HttpGetAsync<T>(
        string requestUri,
        CancellationToken cancellationToken) where T : class
    {
        var request = CreateRequest(HttpMethod.Get, requestUri);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        using var contentStream = await result.Content
            .ReadAsStreamAsync(cancellationToken);

        return await DeserializeAsync<T>(contentStream, cancellationToken);
    }

    protected async Task<T?> HttpPostAsync<T>(
        string requestUri,
        object data,
        CancellationToken cancellationToken) where T : class
    {
        var request = CreateRequest(HttpMethod.Post, requestUri, data);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        using var contentStream = await result.Content
            .ReadAsStreamAsync(cancellationToken);

        return await DeserializeAsync<T>(contentStream, cancellationToken);
    }

    protected async Task HttpPostAsync(
        string requestUri,
        object data,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(HttpMethod.Post, requestUri, data);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );
    }

    protected async Task HttpPutAsync(
        string requestUri,
        object data,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(HttpMethod.Put, requestUri, data);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );
    }

    protected async Task<T?> HttpPutAsync<T>(
        string requestUri,
        object data,
        CancellationToken cancellationToken) where T : class
    {
        var request = CreateRequest(HttpMethod.Put, requestUri, data);

        using var result = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken
        );

        using var contentStream = await result.Content
            .ReadAsStreamAsync(cancellationToken);

        return await DeserializeAsync<T>(contentStream, cancellationToken);
    }

    private static HttpRequestMessage CreateRequest(
        HttpMethod httpMethod,
        string? requestUri)
    {
        return new HttpRequestMessage(httpMethod, requestUri);
    }

    private static HttpRequestMessage CreateRequest(
        HttpMethod httpMethod,
        string? requestUri,
        object data)
    {
        return new HttpRequestMessage(httpMethod, requestUri)
        {
            Content = new JsonContent(Serialize(data))
        };
    }

    private static async Task<T?> DeserializeAsync<T>(
        Stream stream, CancellationToken cancellationToken)
    {
        return await JsonSerializer.DeserializeAsync<T>(
            stream,
            s_serializerOptions,
            cancellationToken
        );
    }

    private static string Serialize(object data)
    {
        return JsonSerializer.Serialize(data);
    }
}