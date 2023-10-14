using Ads.Query.Application.Common.Requests;

namespace Ads.Query.Application.Features.ClassifiedAds.GetClassifiedAd;

public class GetClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
}