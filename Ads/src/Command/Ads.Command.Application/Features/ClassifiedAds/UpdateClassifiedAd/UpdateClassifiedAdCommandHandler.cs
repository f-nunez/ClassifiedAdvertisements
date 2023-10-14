using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.UpdateClassifiedAd;

public class UpdateClassifiedAdCommandHandler
    : IRequestHandler<UpdateClassifiedAdCommand, UpdateClassifiedAdResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public UpdateClassifiedAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
        _eventStore = eventStore;
    }

    public async Task<UpdateClassifiedAdResponse> Handle(
        UpdateClassifiedAdCommand command,
        CancellationToken cancellationToken)
    {
        UpdateClassifiedAdRequest request = command.UpdateClassifiedAdRequest;

        var response = new UpdateClassifiedAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(request.ClassifiedAdId);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.ClassifiedAdId);

        classifiedAd.Update(
            request.Description,
            request.Title,
           _currentUserService.UserId,
            DateTimeOffset.UtcNow
        );

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.Version,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}