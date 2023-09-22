using MediatR;

namespace Ads.Query.Application.Features.GetClassifiedAd;

public record GetClassifiedAdQuery(GetClassifiedAdRequest GetClassifiedAdRequest)
    : IRequest<GetClassifiedAdResponse>;