using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailResponse : BaseResponse
{
    public GetMyAdDetailDto? GetMyAdDetail { get; set; }

    public GetMyAdDetailResponse(Guid correlationId) : base(correlationId)
    {
    }
}