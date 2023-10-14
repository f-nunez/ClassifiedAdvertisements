using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdUpdate;

public class GetMyAdUpdateResponse : BaseResponse
{
    public GetMyAdUpdateDto? GetMyAdUpdate { get; set; }

    public GetMyAdUpdateResponse(Guid correlationId) : base(correlationId)
    {
    }
}