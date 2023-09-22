using System.Reflection;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using Ads.Query.Infrastructure.Persistence.Contexts;
using Ads.Query.Infrastructure.Persistence.Repositories;
using Ads.Query.Infrastructure.ServiceBus;
using Ads.Query.Infrastructure.ServiceBus.Observers;
using Ads.Query.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(configuration);

        services.AddMongoDb(configuration);

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
            mt.AddConsumers(Assembly.GetExecutingAssembly());

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

    private static IServiceCollection AddMongoDb(
        this IServiceCollection services, IConfiguration configuration)
    {
        BsonClassMap.RegisterClassMap<ClassifiedAd>();

        services.AddSingleton<IMongoDbContext, MongoDbContext>(sp =>
        {
            return new MongoDbContext(configuration.GetConnectionString("MongoDbConnection"));
        });

        services.AddScoped<IRepository<ClassifiedAd>, Repository<ClassifiedAd>>();

        return services;
    }
}