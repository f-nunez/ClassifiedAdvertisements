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

    public void Create(
        string id,
        string createdBy,
        DateTimeOffset createdOn,
        string description,
        string title)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentException(
                $"Required input {nameof(id)} was empty.", nameof(id));

        if (string.IsNullOrEmpty(createdBy))
            throw new ArgumentException(
                $"Required input {nameof(createdBy)} was empty.", nameof(createdBy));

        if (createdOn == DateTimeOffset.MinValue)
            throw new ArgumentException(
                $"Required input {nameof(createdOn)} was empty.", nameof(createdOn));

        if (string.IsNullOrEmpty(description))
            throw new ArgumentException(
                $"Required input {nameof(description)} was empty.", nameof(description));

        if (string.IsNullOrEmpty(title))
            throw new ArgumentException(
                $"Required input {nameof(title)} was empty.", nameof(title));

        Apply(
            new ClassifiedAdCreatedV1
            {
                Id = id,
                CreatedBy = createdBy,
                CreatedOn = createdOn,
                Description = description,
                Title = title
            }
        );
    }

    public void Delete(string deletedBy, DateTimeOffset deletedOn)
    {
        if (string.IsNullOrEmpty(deletedBy))
            throw new ArgumentException($"Required input {nameof(deletedBy)} was empty.", nameof(deletedBy));

        if (deletedOn == DateTimeOffset.MinValue)
            throw new ArgumentException($"Required input {nameof(deletedOn)} was empty.", nameof(deletedOn));

        if (!IsActive)
            throw new DeletedException(nameof(ClassifiedAd), Id);

        Apply(
            new ClassifiedAdDeletedV1
            {
                Id = Id,
                DeletedBy = deletedBy,
                DeletedOn = deletedOn
            }
        );
    }

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