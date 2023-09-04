using Ads.Command.Domain.Common;
using Ads.Command.Domain.Exceptions;
using Ads.Common;
using Ads.Common.DomainEvents;

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
        switch (@event)
        {
            case ClassifiedAdCreatedV1 e:
                CreatedBy = e.CreatedBy;
                CreatedOn = e.CreatedOn;
                Description = e.Description;
                Id = e.Id;
                Title = e.Title;
                break;
            case ClassifiedAdDeletedV1 e:
                Id = e.Id;
                IsActive = false;
                UpdatedBy = e.DeletedBy;
                UpdatedOn = e.DeletedOn;
                break;
            case ClassifiedAdPublishedV1 e:
                Id = e.Id;
                PublishedBy = e.PublishedBy;
                PublishedOn = e.PublishedOn;
                UpdatedBy = e.PublishedBy;
                UpdatedOn = e.PublishedOn;
                break;
            case ClassifiedAdUnpublishedV1 e:
                Id = e.Id;
                PublishedBy = null;
                PublishedOn = null;
                UpdatedBy = e.UnpublishedBy;
                UpdatedOn = e.UnpublishedOn;
                break;
            case ClassifiedAdUpdatedV1 e:
                Description = e.Description;
                Id = Id;
                Title = e.Title;
                UpdatedBy = e.UpdatedBy;
                UpdatedOn = e.UpdatedOn;
                break;
            default:
                throw new WhenException(@event.GetType().Name);
        }
    }
}