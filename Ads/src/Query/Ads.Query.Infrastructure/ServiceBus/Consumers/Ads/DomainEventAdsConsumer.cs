using System.Text.Json;
using Ads.Common.DomainEvents;
using Ads.Query.Application.Common.Exceptions;
using Ads.Query.Application.Common.Interfaces;
using Ads.Query.Domain.Entities;
using Contracts.Ads;
using MassTransit;

namespace Ads.Query.Infrastructure.ServiceBus.Consumers;

public class DomainEventAdsConsumer : IConsumer<DomainEventAdsContract>
{
    private readonly IRepository<ClassifiedAd> _repository;

    public DomainEventAdsConsumer(IRepository<ClassifiedAd> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEventAdsContract> context)
    {
        var domainEvent = context.Message;

        switch (domainEvent.EventType)
        {
            case nameof(ClassifiedAdCreatedV1):
                var classifiedAdCreatedV1 = JsonSerializer
                    .Deserialize<ClassifiedAdCreatedV1>(domainEvent.EventPayload!)!;

                var createClassifiedAd = new ClassifiedAd
                {
                    CreatedBy = classifiedAdCreatedV1.CreatedBy,
                    CreatedOn = classifiedAdCreatedV1.CreatedOn,
                    Description = classifiedAdCreatedV1.Description,
                    Id = classifiedAdCreatedV1.Id,
                    IsActive = true,
                    PublishedBy = null,
                    PublishedOn = null,
                    Title = classifiedAdCreatedV1.Title,
                    UpdatedBy = classifiedAdCreatedV1.CreatedBy,
                    UpdatedOn = classifiedAdCreatedV1.CreatedOn,
                    Version = domainEvent.EventVersion
                };

                await _repository.InsertAsync(createClassifiedAd);
                break;

            case nameof(ClassifiedAdDeletedV1):
                var classifiedAdDeletedV1 = JsonSerializer
                    .Deserialize<ClassifiedAdDeletedV1>(domainEvent.EventPayload!)!;

                var deleteClassifiedAd = await _repository
                    .GetByIdAsync(classifiedAdDeletedV1.Id!);

                if (deleteClassifiedAd is null)
                    throw new NotFoundException(nameof(deleteClassifiedAd), classifiedAdDeletedV1.Id);

                deleteClassifiedAd.IsActive = false;
                deleteClassifiedAd.UpdatedBy = classifiedAdDeletedV1.DeletedBy;
                deleteClassifiedAd.UpdatedOn = classifiedAdDeletedV1.DeletedOn;
                deleteClassifiedAd.Version = domainEvent.EventVersion;

                await _repository.UpdateAsync(deleteClassifiedAd);
                break;

            case nameof(ClassifiedAdPublishedV1):
                var classifiedAdPublishedV1 = JsonSerializer
                    .Deserialize<ClassifiedAdPublishedV1>(domainEvent.EventPayload!)!;

                var publishClassifiedAd = await _repository
                    .GetByIdAsync(classifiedAdPublishedV1.Id!);

                if (publishClassifiedAd is null)
                    throw new NotFoundException(nameof(deleteClassifiedAd), classifiedAdPublishedV1.Id);

                publishClassifiedAd.PublishedBy = classifiedAdPublishedV1.PublishedBy;
                publishClassifiedAd.PublishedOn = classifiedAdPublishedV1.PublishedOn;
                publishClassifiedAd.UpdatedBy = classifiedAdPublishedV1.PublishedBy;
                publishClassifiedAd.UpdatedOn = classifiedAdPublishedV1.PublishedOn;
                publishClassifiedAd.Version = domainEvent.EventVersion;

                await _repository.UpdateAsync(publishClassifiedAd);
                break;

            case nameof(ClassifiedAdUnpublishedV1):
                var classifiedAdUnpublishedV1 = JsonSerializer
                    .Deserialize<ClassifiedAdUnpublishedV1>(domainEvent.EventPayload!)!;

                var UnpublishClassifiedAd = await _repository
                    .GetByIdAsync(classifiedAdUnpublishedV1.Id!);

                if (UnpublishClassifiedAd is null)
                    throw new NotFoundException(nameof(deleteClassifiedAd), classifiedAdUnpublishedV1.Id);

                UnpublishClassifiedAd.PublishedBy = null;
                UnpublishClassifiedAd.PublishedOn = null;
                UnpublishClassifiedAd.UpdatedBy = classifiedAdUnpublishedV1.UnpublishedBy;
                UnpublishClassifiedAd.UpdatedOn = classifiedAdUnpublishedV1.UnpublishedOn;
                UnpublishClassifiedAd.Version = domainEvent.EventVersion;

                await _repository.UpdateAsync(UnpublishClassifiedAd);
                break;

            case nameof(ClassifiedAdUpdatedV1):
                var classifiedAdUpdatedV1 = JsonSerializer
                    .Deserialize<ClassifiedAdUpdatedV1>(domainEvent.EventPayload!)!;

                var UpdateClassifiedAd = await _repository
                    .GetByIdAsync(classifiedAdUpdatedV1.Id!);

                if (UpdateClassifiedAd is null)
                    throw new NotFoundException(nameof(deleteClassifiedAd), classifiedAdUpdatedV1.Id);

                UpdateClassifiedAd.Description = classifiedAdUpdatedV1.Description;
                UpdateClassifiedAd.Title = classifiedAdUpdatedV1.Title;
                UpdateClassifiedAd.UpdatedBy = classifiedAdUpdatedV1.UpdatedBy;
                UpdateClassifiedAd.UpdatedOn = classifiedAdUpdatedV1.UpdatedOn;
                UpdateClassifiedAd.Version = domainEvent.EventVersion;

                await _repository.UpdateAsync(UpdateClassifiedAd);
                break;

            default:
                throw new ArgumentNullException(nameof(domainEvent.EventType), $"Event not found for {domainEvent.EventType}");
        }
    }
}