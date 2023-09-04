using Ads.Command.Application.Common.Exceptions;
using Ads.Command.Application.Common.Interfaces;
using Ads.Command.Domain.ClassifiedAdAggregate;
using MediatR;

namespace Ads.Command.Application.Features.DeleteClassifiedAd;

public class DeleteClassifiedAdCommandHandler
    : IRequestHandler<DeleteClassifiedAdCommand, DeleteClassifiedAdResponse>
{
    private readonly IEventStore<ClassifiedAd> _eventStore;

    public DeleteClassifiedAdCommandHandler(IEventStore<ClassifiedAd> eventStore)
    {
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

        classifiedAd.Delete(Guid.NewGuid().ToString(), DateTimeOffset.UtcNow);

        await _eventStore.AppendEventsAsync(
            classifiedAd,
            request.ExpectedVersion,
            request.CorrelationId,
            cancellationToken
        );

        return response;
    }
}