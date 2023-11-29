using Users.Application.Common.Interfaces;
using MassTransit;

namespace Users.Infrastructure.ServiceBus;

public class MassTransitServiceBus : IServiceBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitServiceBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken = default)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        await _publishEndpoint.Publish(message, cancellationToken);
    }
}