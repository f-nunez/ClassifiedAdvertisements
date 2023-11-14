using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ads.Command.Infrastructure.Persistence.Contexts;

public class EventStoreDbContextSeeder
{
    private readonly ILogger<EventStoreDbContextSeeder> _logger;
    private readonly EventStoreDbContext _context;

    public EventStoreDbContextSeeder(
        ILogger<EventStoreDbContextSeeder> logger,
        EventStoreDbContext context)
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