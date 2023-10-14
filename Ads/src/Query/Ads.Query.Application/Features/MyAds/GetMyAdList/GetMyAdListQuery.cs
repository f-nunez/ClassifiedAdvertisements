using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdList;

public record GetMyAdListQuery(
    GetMyAdListRequest GetMyAdListRequest)
    : IRequest<GetMyAdListResponse>;