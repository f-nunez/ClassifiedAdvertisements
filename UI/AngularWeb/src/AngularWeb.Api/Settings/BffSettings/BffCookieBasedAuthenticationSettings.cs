namespace AngularWeb.Api.Settings;

public class BffCookieBasedAuthenticationSettings
{
    public string AuthenticationChallengeScheme { get; set; } = string.Empty;
    public string AuthenticationScheme { get; set; } = string.Empty;
    public string AuthenticationSignOutScheme { get; set; } = string.Empty;
    public string DataProtectionApplicationName { get; set; } = string.Empty;
    public string DataProtectionRedisConnection { get; set; } = string.Empty;
    public string DataProtectionRedisKey { get; set; } = string.Empty;
    public string DistributedCacheRedisConnection { get; set; } = string.Empty;
    public string DistributedCacheRedisInstanceName { get; set; } = string.Empty;
    public string CookieAuthenticationScheme { get; set; } = string.Empty;
    public TimeSpan CookieExpireTimeSpan { get; set; } = TimeSpan.FromMinutes(15);
    public bool CookieHttpOnly { get; set; } = true;
    public string CookieName { get; set; } = string.Empty;
    public SameSiteMode CookieSameSite { get; set; } = SameSiteMode.Strict;
    public CookieSecurePolicy CookieSecurePolicy { get; set; } = CookieSecurePolicy.Always;
    public bool CookieSlidingExpiration { get; set; } = true;
    public TimeSpan LogoutSessionSlidingExpiration { get; set; } = TimeSpan.FromDays(15);
    public string OpenIdConnectAccessDeniedPath { get; set; } = string.Empty;
    public string OpenIdConnectAuthenticationScheme { get; set; } = string.Empty;
    public string OpenIdConnectAuthority { get; set; } = string.Empty;
    public string OpenIdConnectCallbackPath { get; set; } = string.Empty;
    public string OpenIdConnectClientId { get; set; } = string.Empty;
    public string OpenIdConnectClientSecret { get; set; } = string.Empty;
    public bool OpenIdConnectGetClaimsFromUserInfoEndpoint { get; set; }
    public bool OpenIdConnectMapInboundClaims { get; set; }
    public string OpenIdConnectMetadataAddress { get; set; } = string.Empty;
    public string OpenIdConnectRemoteSignOutPath { get; set; } = string.Empty;
    public bool OpenIdConnectRequireHttpsMetadata { get; set; }
    public string OpenIdConnectResponseMode { get; set; } = string.Empty;
    public string OpenIdConnectResponseType { get; set; } = string.Empty;
    public bool OpenIdConnectSaveTokens { get; set; } = true;
    public string OpenIdConnectSignedOutCallbackPath { get; set; } = string.Empty;
    public string OpenIdConnectTokenValidationParametersNameClaimType { get; set; } = string.Empty;
    public string OpenIdConnectTokenValidationParametersRoleClaimType { get; set; } = string.Empty;
    public bool OpenIdConnectTokenValidationParametersValidateAudience { get; set; }
    public bool OpenIdConnectUsePkce { get; set; } = true;
    public bool OpenIdConnectUseTokenLifetime { get; set; }
}