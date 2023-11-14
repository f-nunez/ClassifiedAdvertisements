using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using Ads.Command.Infrastructure.EventStores;
using Ads.Command.Infrastructure.Persistence.Contexts;
using Ads.Command.Infrastructure.Persistence.Repositories;
using Ads.Command.Infrastructure.ServiceBus;
using Ads.Command.Infrastructure.ServiceBus.Observers;
using Ads.Command.Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEventStoreDb(configuration);

        services.AddMassTransit(configuration);

        return services;
    }

    private static IServiceCollection AddEventStoreDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddDbContext<AdsCommandDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("EventStoreConnection"),
                builder => builder.MigrationsAssembly(
                    typeof(AdsCommandDbContext).Assembly.FullName
                )
            )
        );

        services.AddScoped<AdsCommandDbContextSeeder>();

        services.AddScoped<IEventStore<ClassifiedAd>, EventStore<ClassifiedAd>>();

        services.AddScoped<IRepository, Repository>();

        return services;
    }

    private static IServiceCollection AddMassTransit(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMqSetting>(configuration
            .GetSection(typeof(RabbitMqSetting).Name)
            .Get<RabbitMqSetting>()!);

        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddConsumeObserver<LoggingConsumeObserver>();

        services.AddPublishObserver<LoggingPublishObserver>();

        services.AddReceiveObserver<LoggingReceiveObserver>();

        services.AddSendObserver<LoggingSendObserver>();

        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSetting = context
                    .GetRequiredService<IRabbitMqSetting>();

                cfg.Host(rabbitMqSetting.HostAddress);

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}