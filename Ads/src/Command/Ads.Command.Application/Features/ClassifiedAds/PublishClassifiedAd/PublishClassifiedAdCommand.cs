using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;

public record PublishClassifiedAdCommand(
    PublishClassifiedAdRequest PublishClassifiedAdRequest)
    : IRequest<PublishClassifiedAdResponse>;