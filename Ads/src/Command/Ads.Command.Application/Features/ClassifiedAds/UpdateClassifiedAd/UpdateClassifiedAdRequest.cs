using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.UpdateClassifiedAd;

public class UpdateClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public long Version { get; set; }
}