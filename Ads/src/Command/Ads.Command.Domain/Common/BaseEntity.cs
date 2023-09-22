using Ads.Common;

namespace Ads.Command.Domain.Common;

public abstract class BaseEntity<TId> : IEntity
{
    readonly Action<object> _applier = null!;
    public TId? Id { get; set; }
    public bool IsActive { get; set; } = true;

    public void Handle(BaseDomainEvent @event)
    {
        When(@event);
    }

    protected abstract void When(BaseDomainEvent @event);

    protected virtual void Apply(BaseDomainEvent @event)
    {
        When(@event);
        _applier(@event);
    }
}