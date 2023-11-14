using System.Text.Json;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.Common;
using Ads.Command.Infrastructure.Persistence.Repositories;
using Ads.Common;
using Ads.Common.DomainEvents;
using Contracts.Ads;

namespace Ads.Command.Infrastructure.EventStores;

public class EventStore<T> : IEventStore<T> where T : IAggregateRoot
{
    private readonly IRepository _repository;
    private readonly IServiceBus _serviceBus;

    public EventStore(IRepository repository, IServiceBus serviceBus)
    {
        _repository = repository;
        _serviceBus = serviceBus;
    }

    public async Task AppendEventsAsync(
        T aggregateRoot,
        long expectedVersion,
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        IEnumerable<BaseDomainEvent> changes = aggregateRoot.GetChanges();

        if (changes is null || !changes.Any())
            return;

        string streamName = aggregateRoot.GetStreamName();

        var events = await _repository.ReadStreamEventsAsync(streamName);

        if (expectedVersion != -1 && events != null && events.Count > 0 && events[^1].Version != expectedVersion)
            throw new Exception($"Append failed due to expected version. Stream: {streamName}, Expected version: {expectedVersion}, Actual version: {events[^1].Version}");

        foreach (var change in changes)
        {
            expectedVersion++;

            change.Version = expectedVersion;

            var streamState = MapStreamState(change, streamName, expectedVersion);

            var integrationEvent = GetIntegrationEvent(change, expectedVersion, correlationId);

            await _repository.AppendEventAsync(streamState);

            await _serviceBus.PublishAsync(integrationEvent);
        }

        aggregateRoot.ClearChanges();
    }

    public async Task<T?> ReadStreamEventsAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        string streamName = GetStreamName(id);

        List<StreamState>? events = await _repository
            .ReadStreamEventsAsync(streamName, cancellationToken);

        if (events is null)
            return default;

        IEnumerable<BaseDomainEvent> storedEvents = events.Select(Deserialize);

        var aggregate = (T)Activator.CreateInstance(typeof(T), true)!;

        aggregate.LoadStoredEvents(storedEvents);

        return aggregate;
    }

    public static BaseDomainEvent Deserialize(StreamState @event)
    {
        BaseDomainEvent? data;

        switch (@event.EventType)
        {
            case nameof(ClassifiedAdCreatedV1):
                data = JsonSerializer.Deserialize<ClassifiedAdCreatedV1>(@event.EventPayload);
                break;
            case nameof(ClassifiedAdDeletedV1):
                data = JsonSerializer.Deserialize<ClassifiedAdDeletedV1>(@event.EventPayload);
                break;
            case nameof(ClassifiedAdPublishedV1):
                data = JsonSerializer.Deserialize<ClassifiedAdPublishedV1>(@event.EventPayload);
                break;
            case nameof(ClassifiedAdUnpublishedV1):
                data = JsonSerializer.Deserialize<ClassifiedAdUnpublishedV1>(@event.EventPayload);
                break;
            case nameof(ClassifiedAdUpdatedV1):
                data = JsonSerializer.Deserialize<ClassifiedAdUpdatedV1>(@event.EventPayload);
                break;
            default:
                throw new ArgumentNullException(
                    nameof(@event.EventType), $"Not found the event type for {@event.EventType}");
        }

        if (data is null)
            throw new ArgumentNullException(
                nameof(data), $"Cannot deserialize the event type for {@event.EventType}");

        return data;
    }

    private static DomainEventAdsContract GetIntegrationEvent(
        BaseDomainEvent @event,
        long version, Guid correlationId)
    {
        var integrationEvent = new DomainEventAdsContract
        {
            CausationId = correlationId,
            CorrelationId = correlationId,
            Id = Guid.NewGuid(),
            EventPayload = JsonSerializer.Serialize((object)@event),
            EventType = @event.GetType().Name,
            EventVersion = version
        };

        return integrationEvent;
    }

    private static string GetStreamName<TId>(TId aggregateId)
        => $"{typeof(T).Name}-{aggregateId}";

    static StreamState MapStreamState(BaseDomainEvent @event, string streamName, long version)
    {
        var eventData = new StreamState
        {
            CreatedOn = DateTime.UtcNow,
            EventPayload = JsonSerializer.Serialize((object)@event),
            EventType = @event.GetType().Name,
            StreamName = streamName,
            Version = version
        };

        return eventData;
    }
}