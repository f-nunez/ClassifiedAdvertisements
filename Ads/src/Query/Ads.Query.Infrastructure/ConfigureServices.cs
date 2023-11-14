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
}