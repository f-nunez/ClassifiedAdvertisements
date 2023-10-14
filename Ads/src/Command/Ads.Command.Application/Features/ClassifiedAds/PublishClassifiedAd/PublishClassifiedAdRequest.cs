using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.PublishClassifiedAd;

public class PublishClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public long Version { get; set; }
}