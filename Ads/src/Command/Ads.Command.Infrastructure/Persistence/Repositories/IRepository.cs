using EventStore.Client;

namespace Ads.Command.Infrastructure.Persistence.Repositories;

public interface IRepository
{
    Task AppendEventAsync(string streamName, EventData eventData, long expectedVersion, CancellationToken cancellationToken = default);
    Task<List<ResolvedEvent>?> ReadStreamEventsAsync(string streamName, CancellationToken cancellationToken = default);
}