using MediatR;

namespace Ads.Query.Application.Features.MyAds.GetMyAdUpdate;

public record GetMyAdUpdateQuery(
    GetMyAdUpdateRequest GetMyAdUpdateRequest)
    : IRequest<GetMyAdUpdateResponse>;