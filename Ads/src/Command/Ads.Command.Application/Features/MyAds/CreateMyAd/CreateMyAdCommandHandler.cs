using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.CreateMyAd;

public class CreateMyAdCommandHandler
    : IRequestHandler<CreateMyAdCommand, CreateMyAdResponse>
{
    private static readonly int NewVersion = -1;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public CreateMyAdCommandHandler(
        ICurrentUserService currentUserService,
        IEventStore<ClassifiedAd> eventStore)
    {
        _currentUserService = currentUserService;
        _eventStore = eventStore;
    }

    public async Task<CreateMyAdResponse> Handle(
        CreateMyAdCommand command,
        CancellationToken cancellationToken)
    {
        CreateMyAdRequest request = command.CreateMyAdRequest;

        var response = new CreateMyAdResponse(request.CorrelationId);

        var newClassifiedAd = new ClassifiedAd();

        newClassifiedAd.Create(
            Guid.NewGuid().ToString(),
            _currentUserService.UserId,
            DateTimeOffset.UtcNow,
            request.Description,
            request.Title
        );

        await _eventStore.AppendEventsAsync(
            newClassifiedAd,
            NewVersion,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}