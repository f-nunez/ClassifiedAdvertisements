using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public class UpdateClassifiedAdCommandHandler
    : IRequestHandler<UpdateClassifiedAdCommand, UpdateClassifiedAdResponse>
{
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public UpdateClassifiedAdCommandHandler(IEventStore<ClassifiedAd> eventStore)
    {
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
            Guid.NewGuid().ToString(),
            DateTimeOffset.UtcNow
        );

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.ExpectedVersion,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}