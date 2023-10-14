using Ads.Command.Application.Common.Requests;

namespace Ads.Command.Application.Features.ClassifiedAds.CreateClassifiedAd;

public class CreateClassifiedAdRequest : BaseRequest
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}