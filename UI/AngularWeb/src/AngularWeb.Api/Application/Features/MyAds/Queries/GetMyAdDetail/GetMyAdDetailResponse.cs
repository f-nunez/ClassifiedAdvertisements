using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdDetail;

public class GetMyAdDetailResponse : BaseResponse
{
    public GetMyAdDetailDto? GetMyAdDetail { get; set; }

    public GetMyAdDetailResponse(Guid correlationId) : base(correlationId)
    {
    }
}