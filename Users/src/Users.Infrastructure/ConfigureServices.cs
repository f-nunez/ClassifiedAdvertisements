using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Infrastructure.Persistence.Contexts;
using Users.Infrastructure.Persistence.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDatabase(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName
                )
            )
        );

        services.AddScoped<ApplicationDbContextSeeder>();

        services.AddScoped<IRepository<Role>, Repository<Role>>();

        services.AddScoped<IRepository<User>, Repository<User>>();

        services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();

        return services;
    }
}