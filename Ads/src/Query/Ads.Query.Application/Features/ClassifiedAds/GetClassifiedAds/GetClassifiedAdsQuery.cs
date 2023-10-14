using MediatR;

namespace Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAds;

public record GetClassifiedAdsQuery(GetClassifiedAdsRequest GetClassifiedAdsRequest)
    : IRequest<GetClassifiedAdsResponse>;