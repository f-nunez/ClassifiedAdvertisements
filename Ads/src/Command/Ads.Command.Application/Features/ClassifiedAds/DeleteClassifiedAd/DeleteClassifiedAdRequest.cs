using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.DeleteClassifiedAd;

public class DeleteClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public long Version { get; set; }
}