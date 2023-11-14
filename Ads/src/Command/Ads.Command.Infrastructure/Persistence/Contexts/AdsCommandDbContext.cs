using System.Reflection;
using Ads.Command.Infrastructure.EventStores;
using Microsoft.EntityFrameworkCore;

namespace Ads.Command.Infrastructure.Persistence.Contexts;

public class AdsCommandDbContext : DbContext
{
    public AdsCommandDbContext(DbContextOptions<AdsCommandDbContext> options)
        : base(options)
    {
    }

    public DbSet<StreamState> StreamStates => Set<StreamState>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}