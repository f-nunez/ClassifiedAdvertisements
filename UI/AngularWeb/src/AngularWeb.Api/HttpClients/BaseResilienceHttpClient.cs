using Polly;
using Polly.Registry;

namespace AngularWeb.Api.HttpClients;

public class BaseResilienceHttpClient : BaseHttpClient
{
    private readonly ResiliencePipeline _resiliencePipeline;

    public BaseResilienceHttpClient(
        HttpClient httpClient,
        string resiliencePipelineKey,
        ResiliencePipelineProvider<string> resiliencePipelineProvider)
        : base(httpClient)
    {
        _resiliencePipeline = resiliencePipelineProvider
            .GetPipeline(resiliencePipelineKey);
    }

    protected new async Task HttpDeleteAsync(
        string requestUri,
        CancellationToken cancellationToken)
    {
        await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            await base.HttpDeleteAsync(requestUri, ct);
        }, cancellationToken);
    }

    protected new async Task<T?> HttpDeleteAsync<T>(
        string requestUri,
        CancellationToken cancellationToken) where T : class
    {
        return await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            return await base.HttpDeleteAsync<T>(requestUri, ct);
        }, cancellationToken);
    }

    protected new async Task<string> HttpGetAsync(
        string requestUri,
        CancellationToken cancellationToken)
    {
        return await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            return await base.HttpGetAsync(requestUri, ct);
        }, cancellationToken);
    }

    protected new async Task HttpPostAsync(
        string requestUri,
        object data,
        CancellationToken cancellationToken)
    {
        await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            await base.HttpPostAsync(requestUri, data, ct);
        }, cancellationToken);
    }

    protected new async Task<T?> HttpGetAsync<T>(
        string requestUri,
        CancellationToken cancellationToken) where T : class
    {
        return await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            return await base.HttpGetAsync<T>(requestUri, ct);
        }, cancellationToken);
    }

    protected new async Task<T?> HttpPostAsync<T>(
        string requestUri,
        object data,
        CancellationToken cancellationToken) where T : class
    {
        return await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            return await base.HttpPostAsync<T>(requestUri, data, ct);
        }, cancellationToken);
    }

    protected new async Task HttpPutAsync(
        string requestUri,
        object data,
        CancellationToken cancellationToken)
    {
        await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            await base.HttpPutAsync(requestUri, data, cancellationToken);
        }, cancellationToken);
    }

    protected new async Task<T?> HttpPutAsync<T>(
        string requestUri,
        object data,
        CancellationToken cancellationToken) where T : class
    {
        return await _resiliencePipeline.ExecuteAsync(async ct =>
        {
            return await base.HttpPutAsync<T>(requestUri, data, ct);
        }, cancellationToken);
    }
}