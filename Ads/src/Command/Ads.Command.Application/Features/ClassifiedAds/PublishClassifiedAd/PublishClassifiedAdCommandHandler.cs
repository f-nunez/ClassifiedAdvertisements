using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;

public class PublishClassifiedAdCommandHandler
    : IRequestHandler<PublishClassifiedAdCommand, PublishClassifiedAdResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public PublishClassifiedAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
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

        classifiedAd.Publish(_currentUserService.UserId, DateTimeOffset.UtcNow);

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.Version,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}