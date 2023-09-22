using MediatR;

namespace Ads.Query.Application.Features.GetClassifiedAds;

public record GetClassifiedAdsQuery(GetClassifiedAdsRequest GetClassifiedAdsRequest)
    : IRequest<GetClassifiedAdsResponse>;