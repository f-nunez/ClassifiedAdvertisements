using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.UpdateClassifiedAd;

public record UpdateClassifiedAdCommand(
    UpdateClassifiedAdRequest UpdateClassifiedAdRequest)
    : IRequest<UpdateClassifiedAdResponse>;