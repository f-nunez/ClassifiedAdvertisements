using Ads.Common;

namespace Ads.Command.Domain.Common;

public interface IAggregateRoot : IEntity
{
    void ClearChanges();
    IEnumerable<BaseDomainEvent> GetChanges();
    string GetStreamName();
    void LoadStoredEvents(IEnumerable<BaseDomainEvent> storedEvents);
}