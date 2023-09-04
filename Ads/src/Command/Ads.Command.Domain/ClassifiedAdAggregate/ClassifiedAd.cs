using Ads.Command.Domain.Common;
using Ads.Common;

namespace Ads.Command.Domain.ClassifiedAdAggregate;

public class ClassifiedAd : BaseAggregateRoot<string>
{
    public string? CreatedBy { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; } = DateTimeOffset.UtcNow;
    public string? Description { get; private set; }
    public string? PublishedBy { get; private set; }
    public DateTimeOffset? PublishedOn { get; private set; }
    public string? Title { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTimeOffset? UpdatedOn { get; private set; }

    protected override void When(BaseDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}