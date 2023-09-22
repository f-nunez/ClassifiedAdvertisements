using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence.Contexts;

public class OperationalStoreDbContext : PersistedGrantDbContext
{
    public OperationalStoreDbContext(
        DbContextOptions<PersistedGrantDbContext> options)
        : base(options)
    {
    }
}