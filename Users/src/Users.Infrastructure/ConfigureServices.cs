using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Users.Infrastructure.Persistence.Contexts;
using Users.Infrastructure.Persistence.Repositories;
using Users.Infrastructure.ServiceBus;
using Users.Infrastructure.ServiceBus.Observers;
using Users.Infrastructure.Settings;

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