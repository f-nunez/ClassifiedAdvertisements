using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.MyAds.GetMyAdDetail;

public class GetMyAdDetailRequest : BaseRequest
{
    public string? Id { get; set; }
}