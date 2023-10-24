using Ads.Command.Application.Common.Exceptions;
using EventStore.Client;

namespace Ads.Command.Infrastructure.Persistence.Repositories;

public class Repository : IRepository
{
    private readonly EventStoreClient _eventStoreClient;

    public Repository(EventStoreClient eventStoreClient)
    {
        _eventStoreClient = eventStoreClient;
    }

    public async Task AppendEventAsync(
        string streamName,
        EventData eventData,
        long expectedVersion,
        CancellationToken cancellationToken)
    {
        try
        {
            await _eventStoreClient.AppendToStreamAsync(
                streamName: streamName,
                expectedRevision: (ulong)expectedVersion,
                eventData: new EventData[] { eventData },
                cancellationToken: cancellationToken
            );
        }
        catch (WrongExpectedVersionException ex)
        {
            throw new ExpectedVersionException(streamName, ex.ExpectedVersion, ex.ActualVersion);
        }
    }

    public async Task<List<ResolvedEvent>?> ReadStreamEventsAsync(
        string streamName,
        CancellationToken cancellationToken)
    {
        var result = _eventStoreClient.ReadStreamAsync(
            direction: Direction.Forwards,
            streamName: streamName,
            revision: StreamPosition.Start,
            cancellationToken: cancellationToken
        );

        try
        {
            return await result.ToListAsync();
        }
        catch (StreamNotFoundException)
        {
            return null;
        }
    }
}