using MediatR;

namespace Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAd;

public record GetClassifiedAdQuery(GetClassifiedAdRequest GetClassifiedAdRequest)
    : IRequest<GetClassifiedAdResponse>;