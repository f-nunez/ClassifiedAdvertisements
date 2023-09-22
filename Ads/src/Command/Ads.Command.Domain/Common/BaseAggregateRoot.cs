using Ads.Common;

namespace Ads.Command.Domain.Common;

public abstract class BaseAggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
{
    private readonly List<BaseDomainEvent> _changes = new();
    public int Version { get; private set; } = -1;

    public void ClearChanges() => _changes.Clear();

    public IEnumerable<BaseDomainEvent> GetChanges() => _changes.AsEnumerable();

    public string GetStreamName() => $"{GetType().Name}-{Id}";

    public void LoadStoredEvents(IEnumerable<BaseDomainEvent> storedEvents)
    {
        foreach (var storedEvent in storedEvents)
        {
            When(storedEvent);
            Version++;
        }
    }

    protected override void Apply(BaseDomainEvent @event)
    {
        When(@event);
        _changes.Add(@event);
    }

    protected void ApplyToEntity(IEntity entity, BaseDomainEvent @event) => entity?.Handle(@event);
}