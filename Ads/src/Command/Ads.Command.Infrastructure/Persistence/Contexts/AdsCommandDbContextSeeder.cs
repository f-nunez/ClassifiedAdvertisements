using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ads.Command.Infrastructure.Persistence.Contexts;

public class AdsCommandDbContextSeeder
{
    private readonly ILogger<AdsCommandDbContextSeeder> _logger;
    private readonly AdsCommandDbContext _context;

    public AdsCommandDbContextSeeder(
        ILogger<AdsCommandDbContextSeeder> logger,
        AdsCommandDbContext context)
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