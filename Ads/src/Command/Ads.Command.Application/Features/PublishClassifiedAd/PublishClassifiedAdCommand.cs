using MediatR;

namespace Ads.Command.Application.Features.PublishClassifiedAd;

public record PublishClassifiedAdCommand(PublishClassifiedAdRequest PublishClassifiedAdRequest)
    : IRequest<PublishClassifiedAdResponse>;