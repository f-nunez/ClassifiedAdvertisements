using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Contexts;
using Identity.Infrastructure.Settings;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var identityServerSetting = configuration
            .GetSection(typeof(IdentityServerSetting).Name)
            .Get<IdentityServerSetting>()!;

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("IdentityConnection"),
                builder => builder.MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName
                )
            )
        );

        services.AddIdentityServer(options =>
        {
            options.EmitStaticAudienceClaim = identityServerSetting.EmitStaticAudienceClaim;

            options.Events.RaiseErrorEvents = identityServerSetting.RaiseErrorEvents;
            options.Events.RaiseFailureEvents = identityServerSetting.RaiseFailureEvents;
            options.Events.RaiseInformationEvents = identityServerSetting.RaiseInformationEvents;
            options.Events.RaiseSuccessEvents = identityServerSetting.RaiseSuccessEvents;
            options.IssuerUri = identityServerSetting.IssuerUri;
        })
        .AddAspNetIdentity<ApplicationUser>()
        .AddConfigurationStore(configurationStoreOptions =>
        {
            configurationStoreOptions.ConfigureDbContext = options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("IdentityServerConnection"),
                    builder => builder.MigrationsAssembly(
                        typeof(ConfigurationStoreDbContext).Assembly.FullName
                    )
                );
        })
        .AddOperationalStore(operationStoreOptions =>
        {
            operationStoreOptions.ConfigureDbContext = options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("IdentityServerConnection"),
                    UriBuilder => UriBuilder.MigrationsAssembly(
                        typeof(OperationalStoreDbContext).Assembly.GetName().Name
                    )
                );
        });

        services.AddScoped<ConfigurationStoreDbContext>();

        services.AddScoped<OperationalStoreDbContext>();

        services.AddScoped<ApplicationDbContextSeeder>();

        services.AddScoped<ConfigurationStoreDbContextSeeder>();

        services.AddScoped<OperationalStoreDbContextSeeder>();

        var redisConnection = ConnectionMultiplexer.Connect(
            identityServerSetting.DataProtectionRedisConnection);

        services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redisConnection, identityServerSetting.DataProtectionRedisKey)
            .SetApplicationName(identityServerSetting.DataProtectionApplicationName);

        return services;
    }
}