using MediatR;

namespace Ads.Command.Application.Features.DeleteClassifiedAd;

public record DeleteClassifiedAdCommand(DeleteClassifiedAdRequest DeleteClassifiedAdRequest)
    : IRequest<DeleteClassifiedAdResponse>;