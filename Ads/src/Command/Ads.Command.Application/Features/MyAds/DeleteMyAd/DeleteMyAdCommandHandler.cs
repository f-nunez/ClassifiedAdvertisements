using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.DeleteMyAd;

public class DeleteMyAdCommandHandler
    : IRequestHandler<DeleteMyAdCommand, DeleteMyAdResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public DeleteMyAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
        _eventStore = eventStore;
    }

    public async Task<DeleteMyAdResponse> Handle(
        DeleteMyAdCommand command,
        CancellationToken cancellationToken)
    {
        DeleteMyAdRequest request = command.DeleteMyAdRequest;

        var response = new DeleteMyAdResponse(request.CorrelationId);

        var classifiedAd = await _eventStore.ReadStreamEventsAsync(request.Id);

        if (classifiedAd is null)
            throw new NotFoundException(nameof(classifiedAd), request.Id);

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