using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.UpdateMyAd;

public class UpdateMyAdCommandHandler
    : IRequestHandler<UpdateMyAdCommand, UpdateMyAdResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public UpdateMyAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
        _eventStore = eventStore;
    }

    public async Task<UpdateMyAdResponse> Handle(
        UpdateMyAdCommand command,
        CancellationToken cancellationToken)
    {
        UpdateMyAdRequest request = command.UpdateMyAdRequest;

        var response = new UpdateMyAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(
            request.Id, cancellationToken);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

        if (classifiedAd.CreatedBy != _currentUserService.UserId)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

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