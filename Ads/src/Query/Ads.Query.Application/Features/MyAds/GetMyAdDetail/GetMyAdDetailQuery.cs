using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public record GetMyAdDetailQuery(
    GetMyAdDetailRequest GetMyAdDetailRequest)
    : IRequest<GetMyAdDetailResponse>;