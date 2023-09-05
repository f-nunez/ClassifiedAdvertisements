using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.DeleteClassifiedAd;

public class DeleteClassifiedAdRequest : BaseRequest
{
    public string? ClassifiedAdId { get; set; }
    public long ExpectedVersion { get; set; }
}