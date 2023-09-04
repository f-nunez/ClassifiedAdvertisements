using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.PublishClassifiedAd;

public class PublishClassifiedAdCommandHandler : IRequestHandler<PublishClassifiedAdCommand, PublishClassifiedAdResponse>
{
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public PublishClassifiedAdCommandHandler(IEventStore<ClassifiedAd> eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<PublishClassifiedAdResponse> Handle(
        PublishClassifiedAdCommand command,
        CancellationToken cancellationToken)
    {
        PublishClassifiedAdRequest request = command.PublishClassifiedAdRequest;

        var response = new PublishClassifiedAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(request.ClassifiedAdId);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.ClassifiedAdId);

        classifiedAd.Publish(Guid.NewGuid().ToString(), DateTimeOffset.UtcNow);

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.ExpectedVersion,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}