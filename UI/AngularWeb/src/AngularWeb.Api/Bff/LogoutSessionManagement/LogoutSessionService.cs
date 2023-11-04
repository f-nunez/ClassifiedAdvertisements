using System.Text.Json;
using AngularWeb.Api.Bff.Common;
using AngularWeb.Api.Bff.Helpers;
using AngularWeb.Api.Settings;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Distributed;

namespace AngularWeb.Api.Bff.LogoutSessionManagement;

public class LogoutSessionService : ILogoutSessionService
{
    private const string SidClaimType = "sid";
    private const string SubClaimType = "sub";
    private static readonly object s_lock = new();
    private readonly BffCookieBasedAuthenticationSettings _bffSettings;
    private readonly IDistributedCache _distributedCache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<LogoutSessionService> _logger;

    public LogoutSessionService(
        BffCookieBasedAuthenticationSettings bffSettings,
        IDistributedCache distributedCache,
        IHttpClientFactory httpClientFactory,
        ILogger<LogoutSessionService> logger)
    {
        _bffSettings = bffSettings;
        _distributedCache = distributedCache;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<bool> IsLoggedOutAsync(string? sub, string? sid)
    {
        _logger.LogInformation($"IsLoggedOutAsync({nameof(sub)}: {sub}, {nameof(sid)}: {sid})");

        var matches = false;

        var sessionKey = $"{sub}_{sid}";

        try
        {
            var sessionInCache = await _distributedCache.GetStringAsync(sessionKey);

            if (sessionInCache != null)
            {
                var session = JsonSerializer.Deserialize<SessionRecord>(sessionInCache);

                matches = session?.IsMatch(sub, sid) == true;

                _logger.LogInformation($"Session in cache {nameof(matches)}: {matches}, {nameof(sessionKey)}: {sessionKey}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await _distributedCache.RemoveAsync(sessionKey);

            _logger.LogInformation($"Session removed in cache {nameof(matches)}: {matches}, {nameof(sessionKey)}: {sessionKey}");
        }

        return matches;
    }

    public async Task ProcessBackChannelLogout(string logoutToken)
    {
        _logger.LogInformation($"ProcessBackChannelLogout({nameof(logoutToken)}: {logoutToken})");

        var client = _httpClientFactory.CreateClient();

        var disco = await client.GetDiscoveryDocumentAsync(_bffSettings.OpenIdConnectMetadataAddress);

        string audience = _bffSettings.OpenIdConnectClientId;

        var claims = BackChannelLogoutHelper.GetClaimsPrincipal(logoutToken, disco, audience);

        var sub = claims.FindFirst(SubClaimType)?.Value;

        var sid = claims.FindFirst(SidClaimType)?.Value;

        AddLogoutSession(sub, sid);
    }

    private void AddLogoutSession(string? sub, string? sid)
    {
        _logger.LogInformation($"AddLogoutSession({nameof(sub)}:{sub}, {nameof(sid)}: {sid}");

        lock (s_lock)
        {
            try
            {
                string sessionKey = LogoutSessionKeyHelper.GetSessionKey(sub, sid);

                var sessionInCache = _distributedCache.GetString(sessionKey);

                if (!string.IsNullOrEmpty(sessionInCache))
                {
                    _logger.LogInformation($"Exists logout {nameof(sessionKey)}: {sessionKey}");
                }
                else
                {
                    var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(_bffSettings.LogoutSessionSlidingExpiration);

                    var session = new SessionRecord(sub, sid);

                    var sessionJson = JsonSerializer.Serialize(session);

                    _distributedCache.SetString(sessionKey, sessionJson, options);

                    _logger.LogInformation($"Added logout {nameof(sessionKey)}: {sessionKey}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}