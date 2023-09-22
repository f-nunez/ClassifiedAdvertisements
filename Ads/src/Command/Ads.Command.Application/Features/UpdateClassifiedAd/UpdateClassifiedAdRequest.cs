using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.UpdateClassifiedAd;

public class UpdateClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public string? Description { get; set; }
    public long ExpectedVersion { get; set; }
    public string? Title { get; set; }
}