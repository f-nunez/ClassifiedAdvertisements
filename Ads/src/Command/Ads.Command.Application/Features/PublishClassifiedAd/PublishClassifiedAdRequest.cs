using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.PublishClassifiedAd;

public class PublishClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public long ExpectedVersion { get; set; }
}