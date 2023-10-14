using MediatR;

namespace Ads.Command.Application.Features.ClassifiedAds.CreateClassifiedAd;

public record CreateClassifiedAdCommand(
    CreateClassifiedAdRequest CreateClassifiedAdRequest)
    : IRequest<CreateClassifiedAdResponse>;