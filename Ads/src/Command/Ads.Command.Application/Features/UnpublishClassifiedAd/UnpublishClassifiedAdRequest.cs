using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UnpublishClassifiedAd;

public class UnpublishClassifiedAdRequest : BaseRequest
{
    public string ClassifiedAdId { get; set; } = string.Empty;
    public long ExpectedVersion { get; set; } = -1;
}