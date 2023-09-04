using MediatR;

namespace Ads.Command.Application.Features.UnpublishClassifiedAd;

public record UnpublishClassifiedAdCommand(UnpublishClassifiedAdRequest UnpublishClassifiedAdRequest)
    : IRequest<UnpublishClassifiedAdResponse>;