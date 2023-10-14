using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.UnpublishClassifiedAd;

public record UnpublishClassifiedAdCommand(
    UnpublishClassifiedAdRequest UnpublishClassifiedAdRequest)
    : IRequest<UnpublishClassifiedAdResponse>;