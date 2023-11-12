using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace AngularWeb.Api.Handlers;

public class AccessTokenDelegatingHandler : DelegatingHandler
{
    private const string AccessToken = "access_token";
    private const string AuthenticationScheme = "Bearer";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessTokenDelegatingHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext is HttpContext context)
        {
            var accessToken = await context.GetTokenAsync(AccessToken);

            if (accessToken is not null)
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    AuthenticationScheme, accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
