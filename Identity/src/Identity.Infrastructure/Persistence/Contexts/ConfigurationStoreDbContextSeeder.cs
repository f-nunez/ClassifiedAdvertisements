using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Identity.Infrastructure.Settings;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence.Contexts;

public class ConfigurationStoreDbContextSeeder
{
    private readonly IConfiguration _configuration;
    private readonly ConfigurationStoreDbContext _context;
    private readonly ILogger<ConfigurationStoreDbContext> _logger;

    public ConfigurationStoreDbContextSeeder(
        IConfiguration configuration,
        ConfigurationStoreDbContext context,
        ILogger<ConfigurationStoreDbContext> logger)
    {
        _configuration = configuration;
        _context = context;
        _logger = logger;
    }

    public async Task MigrateAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await AddIdentityResourcesAsync();

        await AddApiResourcesAsync();

        await AddApiScopesAsync();

        await AddClientsAsync();
    }

    private async Task AddApiResourcesAsync()
    {
        if (await _context.ApiResources.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new ApiResource
            {
                Name = "87c76d97-4a08-447b-a8e2-6f3b7a2412b1",
                DisplayName = "Angular Web Api",
                Scopes = new List<string> { "angular_web_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "7e2593ba-e3cd-40e5-a50e-506877d0210e",
                DisplayName = "Ads Command Api",
                Scopes = new List<string> { "ads_command_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "717228aa-0fa6-43d1-8e3a-47325d0f57cc",
                DisplayName = "Ads Query Api",
                Scopes = new List<string> { "ads_query_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiResource
            {
                Name = "13b45036-c874-4b21-a651-c48148b78215",
                DisplayName = "Users Api",
                Scopes = new List<string> { "users_api" },
                UserClaims = new List<string>
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddApiScopesAsync()
    {
        if (await _context.ApiScopes.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new ApiScope
            {
                Name = "angular_web_api",
                DisplayName = "Angular Web Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "ads_command_api",
                DisplayName = "Ads Command Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "ads_query_api",
                DisplayName = "Ads Query Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity(),
            new ApiScope
            {
                Name = "users_api",
                DisplayName = "Users Api",
                UserClaims = new[]
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.SessionId
                }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddIdentityResourcesAsync()
    {
        if (await _context.IdentityResources.AnyAsync())
            return;

        await _context.AddRangeAsync(
            new IdentityResources.OpenId().ToEntity(),
            new IdentityResources.Profile().ToEntity(),
            new IdentityResources.Email().ToEntity(),
            new IdentityResource
            {
                Name = "roles",
                DisplayName = "Roles",
                UserClaims = { JwtClaimTypes.Role }
            }.ToEntity()
        );

        await _context.SaveChangesAsync();
    }

    private async Task AddClientsAsync()
    {
        if (await _context.Clients.AnyAsync())
            return;

        var angularWebClientSettings = _configuration
            .GetSection(typeof(AngularWebTokenClientSettings).Name)
            .Get<AngularWebTokenClientSettings>()!;

        var angularWebWithTokenBasedAuth = new Client
        {
            ClientId = "6c4c5801-1089-4c3c-83c7-ddc0eb3707b3",
            ClientName = "Classified Advertisements Angular App",
            // ClientSecrets = new List<Secret> { new("secret".Sha256()) },
            Description = "Classified Advertisements Angular App",
            AllowAccessTokensViaBrowser = false,
            AllowedCorsOrigins = angularWebClientSettings.AllowedCorsOrigins,
            AllowedGrantTypes = GrantTypes.Code,
            AllowOfflineAccess = true,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "roles",
                "ads_command_api",
                "ads_query_api",
                "angular_web_api",
                "users_api"
            },
            // AccessTokenLifetime = 600,
            PostLogoutRedirectUris = angularWebClientSettings.PostLogoutRedirectUris,
            RedirectUris = angularWebClientSettings.RedirectUris,
            // RefreshTokenUsage = TokenUsage.ReUse
            RequireClientSecret = false,
            // RequireConsent = false,
            RequirePkce = true,
            // UpdateAccessTokenClaimsOnRefresh = true,
        };

        var angularWebCookieClientSettings = _configuration
            .GetSection(typeof(AngularWebCookieClientSettings).Name)
            .Get<AngularWebCookieClientSettings>()!;

        var angularWebWithCookieBasedAuth = new Client
        {
            ClientId = "4c534f05-a5a7-4762-9f9d-5d5214f8caaf",
            ClientName = "Classified Advertisements Angular App",
            ClientSecrets = new List<Secret> { new("secret".Sha256()) },
            Description = "Classified Advertisements Angular App",
            AllowAccessTokensViaBrowser = false,
            AllowedCorsOrigins = angularWebCookieClientSettings.AllowedCorsOrigins,
            AllowedGrantTypes = GrantTypes.Code,
            AllowOfflineAccess = true,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "roles",
                "ads_command_api",
                "ads_query_api"
            },
            // AccessTokenLifetime = 60*60,
            // IdentityTokenLifetime = 60*5,
            // SlidingRefreshTokenLifetime = 60*60*24*15,
            // AbsoluteRefreshTokenLifetime = 60*60*24*30,
            // RefreshTokenExpiration = TokenExpiration.Absolute,
            PostLogoutRedirectUris = angularWebCookieClientSettings.PostLogoutRedirectUris,
            RedirectUris = angularWebCookieClientSettings.RedirectUris,
            BackChannelLogoutUri = angularWebCookieClientSettings.BackChannelLogoutUri,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RequireClientSecret = true,
            RequireConsent = false,
            RequirePkce = true,
            UpdateAccessTokenClaimsOnRefresh = true,
            // CoordinateLifetimeWithUserSession = true,
        };

        await _context.AddRangeAsync(
            angularWebWithTokenBasedAuth.ToEntity(),
            angularWebWithCookieBasedAuth.ToEntity()
        );

        await _context.SaveChangesAsync();
    }
}