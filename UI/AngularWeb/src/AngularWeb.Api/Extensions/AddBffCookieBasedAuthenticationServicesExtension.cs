using AngularWeb.Api.Bff.LogoutSessionManagement;
using AngularWeb.Api.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

internal static class AddBffCookieBasedAuthenticationServicesExtension
{
    public static void AddBffCookieBasedAuthenticationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var bffSettings = configuration
            .GetSection(nameof(BffCookieBasedAuthenticationSettings))
            .Get<BffCookieBasedAuthenticationSettings>();

        if (bffSettings is null)
            throw new ArgumentNullException(
                nameof(bffSettings), $"{nameof(bffSettings)} is required.");

        services.AddSingleton(bffSettings);

        var dataProtectionRedisConnection = ConnectionMultiplexer
            .Connect(bffSettings.DataProtectionRedisConnection);

        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(dataProtectionRedisConnection, bffSettings.DataProtectionRedisKey)
            .SetApplicationName(bffSettings.DataProtectionApplicationName);

        var distributedCacheRedisConnection = ConnectionMultiplexer
            .Connect(bffSettings.DistributedCacheRedisConnection);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = distributedCacheRedisConnection.Configuration;
            options.InstanceName = bffSettings.DistributedCacheRedisInstanceName;
        });

        // Implements the cookie event handler
        services.AddTransient<LogoutSessionCookieAuthenticationEvent>();

        // State management to keep track of logout notifications
        services.AddSingleton<ILogoutSessionService, LogoutSessionService>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = bffSettings.AuthenticationScheme;
            options.DefaultChallengeScheme = bffSettings.AuthenticationChallengeScheme;
            options.DefaultSignOutScheme = bffSettings.AuthenticationSignOutScheme;
        })
        .AddCookie(bffSettings.CookieAuthenticationScheme, options =>
        {
            options.Cookie.Name = bffSettings.CookieName;
            options.Cookie.SameSite = bffSettings.CookieSameSite;

            options.Cookie.SecurePolicy = bffSettings.CookieSecurePolicy;
            options.Cookie.HttpOnly = bffSettings.CookieHttpOnly;

            options.ExpireTimeSpan = bffSettings.CookieExpireTimeSpan;
            options.SlidingExpiration = bffSettings.CookieSlidingExpiration;

            options.EventsType = typeof(LogoutSessionCookieAuthenticationEvent);
        })
        .AddOpenIdConnect(bffSettings.OpenIdConnectAuthenticationScheme, options =>
        {
            // Override cookie lifetime based on token data
            options.UseTokenLifetime = bffSettings.OpenIdConnectUseTokenLifetime;

            // Default openid connect paths
            options.CallbackPath = new PathString(bffSettings.OpenIdConnectCallbackPath);
            options.SignedOutCallbackPath = new PathString(bffSettings.OpenIdConnectSignedOutCallbackPath);
            options.RemoteSignOutPath = new PathString(bffSettings.OpenIdConnectRemoteSignOutPath);

            options.AccessDeniedPath = new PathString(bffSettings.OpenIdConnectAccessDeniedPath);

            options.Authority = bffSettings.OpenIdConnectAuthority;
            options.ClientId = bffSettings.OpenIdConnectClientId;
            options.UsePkce = bffSettings.OpenIdConnectUsePkce;
            options.ClientSecret = bffSettings.OpenIdConnectClientSecret;
            options.ResponseType = bffSettings.OpenIdConnectResponseType;
            options.ResponseMode = bffSettings.OpenIdConnectResponseMode;

            options.MetadataAddress = bffSettings.OpenIdConnectMetadataAddress;
            options.RequireHttpsMetadata = bffSettings.OpenIdConnectRequireHttpsMetadata;

            options.MapInboundClaims = bffSettings.OpenIdConnectMapInboundClaims;
            // Keeps id_token smaller
            options.GetClaimsFromUserInfoEndpoint = bffSettings.OpenIdConnectGetClaimsFromUserInfoEndpoint;
            options.SaveTokens = bffSettings.OpenIdConnectSaveTokens;

            // Delete defaults scopes and configure them
            // Which offline_access scope is required to request refresh token
            options.Scope.Clear();
            options.Scope.Add("email");
            options.Scope.Add("openid");
            options.Scope.Add("offline_access");
            options.Scope.Add("profile");
            options.Scope.Add("roles");
            options.Scope.Add("ads_command_api");
            options.Scope.Add("ads_query_api");

            // Map claims not mapped by default
            options.ClaimActions.MapJsonKey("preferred_username", "preferred_username");
            options.ClaimActions.MapJsonKey("role", "role");

            options.TokenValidationParameters.ValidateAudience = bffSettings.OpenIdConnectTokenValidationParametersValidateAudience;

            // Set custom claims instead of default values to get into ClaimsIdentity
            options.TokenValidationParameters.NameClaimType = bffSettings.OpenIdConnectTokenValidationParametersNameClaimType;
            options.TokenValidationParameters.RoleClaimType = bffSettings.OpenIdConnectTokenValidationParametersRoleClaimType;
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("WebApiPolicy", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
            });

            options.AddPolicy("MyAdsPolicy", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireRole(new string[] { "Customer", "Manager", "Staff" });
            });
        });
    }
}