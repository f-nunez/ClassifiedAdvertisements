using Ads.Command.Infrastructure.EventStores;

namespace Ads.Command.Infrastructure.Persistence.Repositories;

public interface IRepository
{
    Task AppendEventAsync(StreamState streamState, CancellationToken cancellationToken = default);
    Task<List<StreamState>?> ReadStreamEventsAsync(string streamName, CancellationToken cancellationToken = default);
}