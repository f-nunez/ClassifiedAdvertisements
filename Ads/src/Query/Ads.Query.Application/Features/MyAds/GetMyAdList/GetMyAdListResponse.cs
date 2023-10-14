using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdList;

public class GetMyAdListResponse : BaseResponse
{
    public DataTableResponse<GetMyAdListItemDto>? DataTableResponse { get; set; }

    public GetMyAdListResponse(Guid correlationId) : base(correlationId)
    {
    }
}