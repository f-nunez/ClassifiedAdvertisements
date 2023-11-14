using System.Reflection;
using Ads.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ads.Query.Infrastructure.Persistence.Contexts;

public class AdsQueryDbContext : DbContext
{
    public AdsQueryDbContext(DbContextOptions<AdsQueryDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClassifiedAd> ClassifiedAds => Set<ClassifiedAd>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}