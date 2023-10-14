using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;

public class DeleteClassifiedAdCommandHandler
    : IRequestHandler<DeleteClassifiedAdCommand, DeleteClassifiedAdResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public DeleteClassifiedAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
        _eventStore = eventStore;
    }

    public async Task<DeleteClassifiedAdResponse> Handle(
        DeleteClassifiedAdCommand command,
        CancellationToken cancellationToken)
    {
        DeleteClassifiedAdRequest request = command.DeleteClassifiedAdRequest;

        var response = new DeleteClassifiedAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(request.ClassifiedAdId);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.ClassifiedAdId);

        classifiedAd.Delete(_currentUserService.UserId, DateTimeOffset.UtcNow);

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.Version,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}