using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdList;

public class GetMyAdListResponse : BaseResponse
{
    public DataTableResponse<GetMyAdListItemDto>? DataTableResponse { get; set; }

    public GetMyAdListResponse(Guid correlationId) : base(correlationId)
    {
    }
}