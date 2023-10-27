using AngularWeb.Api.Application.Common.Requests;

namespace AngularWeb.Api.Application.Features.MyAds.Queries.GetMyAdUpdate;

public class GetMyAdUpdateResponse : BaseResponse
{
    public GetMyAdUpdateDto? GetMyAdUpdate { get; set; }

    public GetMyAdUpdateResponse(Guid correlationId) : base(correlationId)
    {
    }
}