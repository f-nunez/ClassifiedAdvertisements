using Ads.Command.Infrastructure.EventStores;
using Ads.Command.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ads.Command.Infrastructure.Persistence.Repositories;

public class Repository : IRepository
{
    private readonly EventStoreDbContext _dbContext;

    public Repository(EventStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AppendEventAsync(StreamState streamState, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(streamState, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<StreamState>?> ReadStreamEventsAsync(string streamName, CancellationToken cancellationToken)
    {
        var query = from streamState in _dbContext.StreamStates
                    where streamState.StreamName == streamName
                    orderby streamState.Version
                    select streamState;

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }
}