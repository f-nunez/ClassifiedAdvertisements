using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdUpdate;

public class GetMyAdUpdateRequest : BaseRequest
{
    public string? Id { get; set; }
}