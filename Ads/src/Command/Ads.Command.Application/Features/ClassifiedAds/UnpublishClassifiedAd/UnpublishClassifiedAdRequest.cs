using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.UnpublishClassifiedAd;

public class UnpublishClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public long Version { get; set; }
}