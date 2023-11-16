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
        services.AddDatabase(configuration);

        services.AddMassTransitServiceBus(configuration);

        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddDbContext<AdsCommandDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("AdsCommandDbConnection"),
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

    private static IServiceCollection AddMassTransitServiceBus(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IServiceBus, MassTransitServiceBus>();

        services.AddConsumeObserver<LoggingConsumeObserver>();

        services.AddPublishObserver<LoggingPublishObserver>();

        services.AddReceiveObserver<LoggingReceiveObserver>();

        services.AddSendObserver<LoggingSendObserver>();

        var serviceBusSettings = configuration
            .GetSection(typeof(ServiceBusSettings).Name)
            .Get<ServiceBusSettings>();

        if (serviceBusSettings is null)
            throw new ArgumentNullException(
                nameof(serviceBusSettings), $"{nameof(serviceBusSettings)} is required.");

        switch (serviceBusSettings.ServiceBusType)
        {
            case ServiceBusType.AzureServiceBus:
                services.AddMassTransit(mt =>
                {
                    mt.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(serviceBusSettings.HostAddress);

                        cfg.ConfigureEndpoints(context);
                    });
                });
                break;
            case ServiceBusType.RabbitMQ:
                services.AddMassTransit(mt =>
                {
                    mt.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(serviceBusSettings.HostAddress);

                        cfg.ConfigureEndpoints(context);
                    });
                });
                break;
            default:
                throw new ArgumentException(
                    $"Not found {nameof(serviceBusSettings.ServiceBusType)}: {serviceBusSettings.ServiceBusType}");
        }

        return services;
    }
}