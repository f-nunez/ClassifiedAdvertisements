using Ads.Command.Domain.Common;

namespace Ads.Command.Application.Common.Interfaces;

public interface IEventStore<T> where T : IAggregateRoot
{
    Task AppendEventsAsync(T aggregateRoot, long expectedVersion, Guid correlationId, CancellationToken cancellationToken = default);
    Task<T> ReadStreamEventsAsync<TId>(TId id, CancellationToken cancellationToken = default);
}