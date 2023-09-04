using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.CreateClassifiedAd;

public class CreateClassifiedAdCommandHandler
    : IRequestHandler<CreateClassifiedAdCommand, CreateClassifiedAdResponse>
{
    private static readonly int NewVersion = -1;
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public CreateClassifiedAdCommandHandler(IEventStore<ClassifiedAd> eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<CreateClassifiedAdResponse> Handle(
        CreateClassifiedAdCommand command,
        CancellationToken cancellationToken)
    {
        CreateClassifiedAdRequest request = command.CreateClassifiedAdRequest;

        var response = new CreateClassifiedAdResponse(request.CorrelationId);

        var newClassifiedAd = new ClassifiedAd();

        newClassifiedAd.Create(
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
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