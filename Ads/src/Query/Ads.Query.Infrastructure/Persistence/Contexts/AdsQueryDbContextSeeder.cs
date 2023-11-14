using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ads.Query.Infrastructure.Persistence.Contexts;

public class AdsQueryDbContextSeeder
{
    private readonly ILogger<AdsQueryDbContextSeeder> _logger;
    private readonly AdsQueryDbContext _context;

    public AdsQueryDbContextSeeder(
        ILogger<AdsQueryDbContextSeeder> logger,
        AdsQueryDbContext context)
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