using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public class UpdateClassifiedAdRequest : BaseRequest
{
    public string ClassifiedAdId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long ExpectedVersion { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
}