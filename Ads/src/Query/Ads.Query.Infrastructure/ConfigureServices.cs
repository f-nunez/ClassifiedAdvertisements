using System.Reflection;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using Ads.Query.Infrastructure.Persistence.Contexts;
using Ads.Query.Infrastructure.Persistence.Repositories;
using Ads.Query.Infrastructure.ServiceBus;
using Ads.Query.Infrastructure.ServiceBus.Observers;
using Ads.Query.Infrastructure.Settings;
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
        services.AddDbContext<AdsQueryDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("AdsQueryDbConnection"),
                builder => builder.MigrationsAssembly(
                    typeof(AdsQueryDbContext).Assembly.FullName
                )
            )
        );

        services.AddScoped<AdsQueryDbContextSeeder>();

        services.AddScoped<IRepository<ClassifiedAd>, Repository<ClassifiedAd>>();

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
                    mt.AddConsumers(Assembly.GetExecutingAssembly());

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
                    mt.AddConsumers(Assembly.GetExecutingAssembly());

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