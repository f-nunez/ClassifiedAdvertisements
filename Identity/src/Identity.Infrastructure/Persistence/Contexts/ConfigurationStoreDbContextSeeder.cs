using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence.Contexts;

public class ConfigurationStoreDbContextSeeder
{
    private readonly ILogger<ConfigurationStoreDbContext> _logger;
    private readonly ConfigurationStoreDbContext _context;

    public ConfigurationStoreDbContextSeeder(
        ILogger<ConfigurationStoreDbContext> logger,
        ConfigurationStoreDbContext context)
    {
        _logger = logger;
        _context = context;
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
                Name = "7e2593ba-e3cd-40e5-a50e-506877d0210e",
                DisplayName = "Ads Api",
                Scopes = new List<string> { "ads_api" },
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
                Name = "ads_api",
                DisplayName = "Ads Api",
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

        var demoAngularApp = new Client
        {
            ClientId = "6c4c5801-1089-4c3c-83c7-ddc0eb3707b3",
            ClientName = "Classified Advertisements Angular App",
            Description = "Classified Advertisements Angular App",
            AllowAccessTokensViaBrowser = true,
            AllowedCorsOrigins = new List<string> { "http://localhost:4200" },
            AllowedGrantTypes = GrantTypes.Code,
            // AllowOfflineAccess = true,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                // IdentityServerConstants.StandardScopes.OfflineAccess,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "roles",
                "ads_api"
            },
            // AccessTokenLifetime = 600,
            PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback-oidc" },
            RedirectUris = new List<string> { "http://localhost:4200", "http://localhost:4200/signin-callback", "http://localhost:4200/silent-callback.html" },
            // RefreshTokenUsage = TokenUsage.ReUse
            RequireClientSecret = false,
            // RequireConsent = false,
            RequirePkce = true,
            // UpdateAccessTokenClaimsOnRefresh = true,
        };

        await _context.AddRangeAsync(
            demoAngularApp.ToEntity()
        );

        await _context.SaveChangesAsync();
    }
}