using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;

public record DeleteClassifiedAdCommand(
    DeleteClassifiedAdRequest DeleteClassifiedAdRequest)
    : IRequest<DeleteClassifiedAdResponse>;