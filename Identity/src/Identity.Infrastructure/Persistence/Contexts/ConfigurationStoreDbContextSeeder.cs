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
}