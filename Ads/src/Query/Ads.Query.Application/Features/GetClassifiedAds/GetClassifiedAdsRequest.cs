using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.GetClassifiedAds;

public class GetClassifiedAdsRequest : BaseRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}