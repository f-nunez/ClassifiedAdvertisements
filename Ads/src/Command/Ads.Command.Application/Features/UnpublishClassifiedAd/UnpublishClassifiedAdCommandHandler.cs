using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.UnpublishClassifiedAd;

public class UnpublishClassifiedAdCommandHandler
    : IRequestHandler<UnpublishClassifiedAdCommand, UnpublishClassifiedAdResponse>
{
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public UnpublishClassifiedAdCommandHandler(IEventStore<ClassifiedAd> eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<UnpublishClassifiedAdResponse> Handle(
        UnpublishClassifiedAdCommand command,
        CancellationToken cancellationToken)
    {
        UnpublishClassifiedAdRequest request = command.UnpublishClassifiedAdRequest;

        var response = new UnpublishClassifiedAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(request.ClassifiedAdId);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.ClassifiedAdId);

        classifiedAd.Unpublish(Guid.NewGuid().ToString(), DateTimeOffset.UtcNow);

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.ExpectedVersion,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}